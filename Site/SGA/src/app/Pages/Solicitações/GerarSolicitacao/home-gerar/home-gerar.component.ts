import { Component, OnInit } from '@angular/core';
import { Empresa } from '../../../../Models/Empresa';
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

interface Sistema {
  name: string;
  selected: boolean;
}


@Component({
  selector: 'app-home-gerar',
  templateUrl: './home-gerar.component.html',
  styleUrl: './home-gerar.component.scss'
})
export class HomeGerarComponent implements OnInit {
  selected: Date | null;
  lstEmpresas = new MatTableDataSource<Empresa>();
  lstDepartamentos = new MatTableDataSource<Departamento>();
  lstCargos = new MatTableDataSource<Cargo>();
  
  
  
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
      { name: 'Apdata', selected: false },
      { name: 'iPlan', selected: false },
      { name: 'TOTVS', selected: false },
      { name: 'WipTracker', selected: false },
      { name: 'MCI', selected: false },
      { name: 'iQuote', selected: false },
      { name: 'JobTrack', selected: false },
      { name: 'Natura', selected: false },
    ];
  
    sistemas.forEach(sistema => {
      sistemasArr.push(this.formBuilder.group({
        name: sistema.name,
        selected: [sistema.selected]
      }));
    });
  }
    
  empresaSelecionada: Empresa = {
    id: 0,
    nome: '',
    email: '',
    telefone: '',
    cep: '',
    endereco: '',
    numero: 0,
    complemento: '',
    bairro: '',
    cidade: '',
    estado: '',
    ativo: false
  }

  departamentoSelecionado: Departamento = {
    id: 0,
    nome: '',
    centroCusto: 0,
    idEmpresa: 0,
    nomeEmpresa: 0
  }

  constructor(
    private cargoService: CargoService,
    private departamentoService: DepartamentoService,
    private empresaService: EmpresaService,
    private alertService: AlertService,
    private toast: HotToastService,
    private formBuilder: FormBuilder,
    private route: Router,
    private activatedRoute: ActivatedRoute,
    ) {}

    public tableForm: FormGroup = this.formBuilder.group({
      nome: ['',Validators.required],
      codigo: ['',Validators.required],
      dataInicio: ['',Validators.required],
      tipoContrato: ['',Validators.required],
      centroCusto: [''],
      idEmpresa: ['',Validators.required],
      idDepartamento: ['',Validators.required],
      idCargo: ['',Validators.required],
      superior: ['',Validators.required],
      acessoInternet: ['',Validators.required],
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
        this.lstEmpresas.data = report as Empresa[];
        this.lstEmpresas.data = this.lstEmpresas.data.filter(x=> x.ativo === true);        
      });
  }

  public onChangeEmpresa(event: any){
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
    const idEmpresa = parseInt(value);
    this.empresaSelecionada.id = idEmpresa
    let resp = this.departamentoService.getAllByEmpresa(idEmpresa).pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os departamentos desta empresa.');
        return of([])
      }));

      resp.subscribe((departamentos: Departamento[]) => {
        this.lstDepartamentos.data = departamentos.filter(d => d.idEmpresa === idEmpresa);
      });
  }

  public onChangeDepartamento(event: any){
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
      const idDepartamento = parseInt(value);
      this.departamentoSelecionado.id = idDepartamento
      let resp = this.cargoService.getAllByDepartamento(idDepartamento).pipe(
        catchError(error =>{
          this.toast.error('Erro ao carregar os cargos deste departamento.');
          return of([])
        }));
  
        resp.subscribe((cargos: Cargo[]) => {
          this.lstCargos.data = cargos.filter(c => c.idDepartamento === idDepartamento);
        });
  
  }

  

  
  onSubmit(): void{
    const sistemasSelecionados = (this.tableForm.get('sistemas') as FormArray).value
    .filter((s: Sistema) => s.selected)
    .map((s: Sistema) => s.name); 

    console.log(this.tableForm.value,sistemasSelecionados);
  }
}
