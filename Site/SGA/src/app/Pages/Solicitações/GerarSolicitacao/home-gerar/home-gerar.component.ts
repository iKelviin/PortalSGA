import { Solicitacao } from './../../../../Models/Solicitacao';
import { Component, OnInit } from '@angular/core';
import { Empresa } from './../../../../Models/Empresa';
import { Departamento } from '../../../../Models/Departamento';
import { MatTableDataSource } from '@angular/material/table';
import { CargoService } from '../../../../Services/cargo.service';
import { DepartamentoService } from '../../../../Services/departamento.service';
import { EmpresaService } from '../../../../Services/empresa.service';
import { AlertService } from '../../../../Shared/Services/alert.service';
import { HotToastService } from '@ngneat/hot-toast';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { Cargo } from '../../../../Models/Cargo';
import { SolicitacaoService } from '../../../../Services/solicitacao.service';

interface Sistema {
  id: number;
  nome: string;
  selected: boolean;
}


@Component({
  selector: 'app-home-gerar',
  templateUrl: './home-gerar.component.html',
  styleUrl: './home-gerar.component.scss'
})
export class HomeGerarComponent implements OnInit {
  selected: Date | null;
  lstEmpresas: Empresa[];
  lstDepartamentos: Departamento[];
  lstCargos: Cargo[];
  lstTipoContratos = [
    {id: 1,nome: 'Profissional'},
    {id: 2,nome: 'Terceiro'},
    {id: 3,nome: 'Temporario'}
  ]
  empresaSelecionada = {} as Empresa;
  departamentoSelecionado = {} as Departamento;
  
  
  
  ngOnInit(): void {
    this.listarEmpresas();
    this.initSistemas();
  }

  get sistemasFormArray() {
    return this.tableForm.get('sistemas') as FormArray;
  }

  initSistemas() {
    const sistemasArr = this.sistemasFormArray;
    const sistemas = [
      {id:1 ,nome: 'Apdata', selected: false },
      {id:2 ,nome: 'iPlan', selected: false },
      {id:3 ,nome: 'Natura', selected: false },
      {id:4 ,nome: 'JobTrack', selected: false },
      {id:5 ,nome: 'iQuote', selected: false },
      {id:6 ,nome: 'MCI', selected: false },
      {id:7 ,nome: 'WipTracker', selected: false },
      {id:8 ,nome: 'TOTVS', selected: false },
    ];
  
    sistemas.forEach(sistema => {
      sistemasArr.push(this.formBuilder.group({
        id: [sistema.id],
        nome: [sistema.nome],
        selected: [sistema.selected]
      }));
    });
  }
    


  constructor(
    private cargoService: CargoService,
    private departamentoService: DepartamentoService,
    private empresaService: EmpresaService,
    private solicitacaoService: SolicitacaoService,
    private toast: HotToastService,
    private formBuilder: FormBuilder,
    ) {}

    public tableForm: FormGroup = this.formBuilder.group({
      nome: ['',Validators.required],
      codigo: ['',Validators.required],
      dataInicio: ['',Validators.required],
      tipoContrato: ['',Validators.required],
      centroCusto: [{value: '', disabled: true}],
      idEmpresa: ['',Validators.required],
      idDepartamento: ['',Validators.required],
      idCargo: ['',Validators.required],
      superior: ['',Validators.required],
      acessoInternet: [false],
      acessoEmail: ['',Validators.required],
      sistemas: this.formBuilder.array([])
    })

  listarEmpresas(){
    let resp = this.empresaService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar as empresas');
        return of([])
      }));

      resp.subscribe((report) => {
        this.lstEmpresas = report as Empresa[];
        this.lstEmpresas = this.lstEmpresas.filter(x=> x.ativo === true);        
      });
  }

  public onChangeEmpresa(event: any){
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
    const idEmpresa = parseInt(value);
    this.empresaSelecionada.id = idEmpresa

    this.tableForm.get('centroCusto')?.setValue('');

    let resp = this.departamentoService.getAllByEmpresa(idEmpresa).pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os departamentos desta empresa.');
        return of([])
      }));

      resp.subscribe((departamentos: Departamento[]) => {
        this.lstDepartamentos = departamentos.filter(d => d.idEmpresa === idEmpresa);
      });
  }

  public onChangeDepartamento(event: any){
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
      const idDepartamento = parseInt(value);
      this.departamentoSelecionado = this.lstDepartamentos.find(x=> x.id == idDepartamento)!;

      this.tableForm.get('centroCusto')?.setValue(this.departamentoSelecionado.centroCusto);

      let resp = this.cargoService.getAllByDepartamento(idDepartamento).pipe(
        catchError(error =>{
          this.toast.error('Erro ao carregar os cargos deste departamento.');
          return of([])
        }));
  
        resp.subscribe((cargos: Cargo[]) => {
          this.lstCargos = cargos.filter(c => c.idDepartamento === idDepartamento);
        });
  
  }

  

  
  onSubmit(): void{
    const formData = this.tableForm.getRawValue();
    const sistemasSelecionados = (this.tableForm.get('sistemas') as FormArray).value
    .filter((s: Sistema) => s.selected)
    .map((s: Sistema) => ({id: s.id ,nome: s.nome})); 

    console.log(formData,sistemasSelecionados);
    let solicitacao: Solicitacao = {
      id: 0,
      codigo: formData.codigo,
      acessoEmail: formData.acessoEmail,
      acessoInternet: formData.acessoInternet,
      dataInicio: formData.dataInicio,
      status: 1,
      statusEmail: 0,
      solicitante: 'Kelvin',
      nomeCompleto: formData.nome,
      idEmpresa: formData.idEmpresa,
      idCargo: formData.idCargo,
      superior: formData.superior,
      tipoContrato: formData.tipoContrato,
      centroCusto: formData.centroCusto,
      sistemas: sistemasSelecionados,
      idDepartamento: formData.idDepartamento
    }

    console.log(solicitacao);
    let resp = this.solicitacaoService.post(solicitacao).pipe(
      this.toast.observe({
        loading: 'Enviando Solicitação...',
        success: 'Solicitação Gerada com Sucesso!',
        error: 'Falha ao gerar a solicitação.'
      })
     ).subscribe({
      next: () => {
        this.tableForm.reset;
      },
      error: (error) => {
        console.error('Erro ao gerar solicitação:', error);
      }
    });
  }
}
