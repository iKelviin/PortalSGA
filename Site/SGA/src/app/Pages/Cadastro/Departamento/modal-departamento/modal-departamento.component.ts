import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UsersData } from '../../Empresa/modal-empresa/modal-empresa.component';
import { Empresa } from '../../../../Models/Empresa';
import { MatTableDataSource } from '@angular/material/table';
import { EmpresaService } from '../../../../Services/empresa.service';
import { catchError, of } from 'rxjs';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-modal-departamento',
  templateUrl: './modal-departamento.component.html',
  styleUrl: './modal-departamento.component.scss'
})
export class ModalDepartamentoComponent implements OnInit{
  action: string;
  local_data: any;
  cancel: string = 'Cancel';
  lstEmpresas = new MatTableDataSource<Empresa>();  

  tableForm!: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ModalDepartamentoComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsersData,
    private formBuilder: FormBuilder,
    private empresaService: EmpresaService,
    private toast: HotToastService,
  ) {
    this.local_data = { ...data };
    this.action = this.local_data.action;
    this.creatForm();
    this.tableForm.patchValue(this.local_data);
  }
  
  ngOnInit(): void {
    this.listarEmpresas();
  }

  listarEmpresas(){
    let resp = this.empresaService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os clientes');
        return of([])
      }));

      resp.subscribe((report) => {
        this.lstEmpresas.data = report as Empresa[];
        this.lstEmpresas.data = this.lstEmpresas.data.filter(x=> x.ativo === true);        
      });
  }
  creatForm(): void {
    this.tableForm = this.formBuilder.group({
      nome: ['', Validators.required],      
      centroCusto: ['',[Validators.required,Validators.pattern(/^[0-9]*$/)]],
      idEmpresa: ['',Validators.required]
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

}
