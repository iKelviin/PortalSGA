import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UsersData } from '../../Empresa/modal-empresa/modal-empresa.component';
import { Empresa } from '../../../../Models/Empresa';
import { MatTableDataSource } from '@angular/material/table';
import { EmpresaService } from '../../../../Services/empresa.service';
import { catchError, of } from 'rxjs';
import { HotToastService } from '@ngneat/hot-toast';
import { Departamento } from '../../../../Models/Departamento';
import { DepartamentoService } from '../../../../Services/departamento.service';

@Component({
  selector: 'app-modal-cargo',
  templateUrl: './modal-cargo.component.html',
  styleUrl: './modal-cargo.component.scss'
})
export class ModalCargoComponent implements OnInit{
  action: string;
  local_data: any;
  cancel: string = 'Cancel';
  lstEmpresas = new MatTableDataSource<Empresa>();  
  lstDepartamentos = new MatTableDataSource<Departamento>();  

  tableForm!: FormGroup;

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


  constructor(
    public dialogRef: MatDialogRef<ModalCargoComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsersData,
    private formBuilder: FormBuilder,
    private empresaService: EmpresaService,
    private departamentoService: DepartamentoService,
    private toast: HotToastService,
  ) {
    this.local_data = { ...data };
    this.action = this.local_data.action;
    this.creatForm();
    this.tableForm.patchValue(this.local_data);
  }
  
  ngOnInit(): void {
    this.listarEmpresas();
    this.listarDepartamentos();
  }

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

  listarDepartamentos(){
    let resp = this.departamentoService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os departamentos');
        return of([])
      }));

      resp.subscribe((report) => {
        this.lstDepartamentos.data = report as Departamento[];      
      });
  }

  creatForm(): void {
    this.tableForm = this.formBuilder.group({
      nome: ['', Validators.required],      
      idEmpresa: ['',Validators.required],
      idDepartamento: ['',Validators.required]
    });
  }

  closeDialog() {
    this.dialogRef.close({ data: { action: 'Cancel' } });
  }

  onSubmit(): void {
    const formData = this.tableForm.value;
    console.log('formData:',formData);

    if(this.local_data.id == null){
      formData.id = 0;
    }else{

      formData.id = this.local_data.id; 
    }


    this.dialogRef.close({ data: { action: this.action, data: formData } });
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

}