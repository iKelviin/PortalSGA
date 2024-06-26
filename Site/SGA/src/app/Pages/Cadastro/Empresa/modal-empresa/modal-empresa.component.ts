import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { emailValidator } from '../../../../Shared/Utils/Validators/emailValidator';
import { telefoneValidator } from '../../../../Shared/Utils/Validators/telefoneValidator';
import { cepValidator } from '../../../../Shared/Utils/Validators/cepValidator';
import { CEPError, Endereco, NgxViacepService } from '@brunoc/ngx-viacep';
import { EMPTY, catchError } from 'rxjs';

export interface UsersData {
  name: string;
  id: number;
}

@Component({
  selector: 'app-modal-empresa',
  templateUrl: './modal-empresa.component.html',
  styleUrl: './modal-empresa.component.scss'
})

export class ModalEmpresaComponent  implements OnInit{
  public cepInvalido: boolean;
  action: string;
  local_data: any;
  cancel: string = 'Cancel';

  tableForm!: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ModalEmpresaComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsersData,
    private formBuilder: FormBuilder,
    private viacep: NgxViacepService
  ) {
    this.local_data = { ...data };
    this.action = this.local_data.action;
    this.creatForm();
    this.tableForm.patchValue(this.local_data);
  }
  ngOnInit(): void {
    console.log("Local Data Dialog Modal:",this.local_data);

  }

  creatForm(): void {
    this.tableForm = this.formBuilder.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, emailValidator()]],
      telefone: ['', [telefoneValidator()]],
      cep: ['', [Validators.required,cepValidator()]],
      endereco: ['', Validators.required],
      numero: ['', [Validators.pattern(/^[0-9]*$/)]],
      complemento: [''],
      cidade: ['', Validators.required],
      bairro: ['', Validators.required],
      estado: ['', Validators.required],
      ativo: [true],
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

  VerificaStatus(): string {
    const statusControl = this.tableForm.get('ativo');
    return statusControl && statusControl.value ? 'Ativo' : 'Inativo';
  }

  getEndereco() {
    let cep: string = this.tableForm.get('cep')?.value;
    this.viacep.buscarPorCep(cep)
    .pipe(
      catchError((error: CEPError) => {
        console.log(error.message);
        this.cepInvalido = true;
        return EMPTY;
      })
    )
    .subscribe((endereco: Endereco) => {
      this.cepInvalido = false;
      this.tableForm.controls['endereco'].setValue(endereco.logradouro);
      this.tableForm.controls['bairro'].setValue(endereco.bairro);
      this.tableForm.controls['cidade'].setValue(endereco.localidade);
      this.tableForm.controls['estado'].setValue(endereco.uf);
    });
  }

}
