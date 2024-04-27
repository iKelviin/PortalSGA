import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { emailValidator } from '../../../../Shared/Utils/Validators/emailValidator';
import { telefoneValidator } from '../../../../Shared/Utils/Validators/telefoneValidator';
import { cepValidator } from '../../../../Shared/Utils/Validators/cepValidator';

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

  action: string;
  local_data: any;
  countries!: string[];
  cancel: string = 'Cancel';

  tableForm!: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ModalEmpresaComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsersData,
    private formBuilder: FormBuilder,) {
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
      numero: ['', Validators.required],
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
    /*this.dialogRef.close({
      data: { action: this.action, data: this.tableForm.value}});*/

      const formData = this.tableForm.value;
      console.log('formData:',formData);

      if(this.local_data.id == null){
        //const novoId = uuidv4();
        //formData.id = novoId;
        formData.id = 0;
        //console.log("novo ID: ", novoId);
      }else{

        formData.id = this.local_data.id; // Defina o ID antes de enviar os dados
        console.log("local_data:",this.local_data);
        console.log("Submit - formData", this.tableForm.value);
      }


      this.dialogRef.close({ data: { action: this.action, data: formData } });
  }

  VerificaStatus(): string {
    const statusControl = this.tableForm.get('ativo');
    return statusControl && statusControl.value ? 'Ativo' : 'Inativo';
  }

}
