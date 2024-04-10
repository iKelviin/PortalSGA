import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCommonModule } from '@angular/material/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';


export interface UsersData {
  name: string;
  id: number;
}

@Component({
  selector: 'app-modal-empresa',
  standalone: true,
  imports: [MatCommonModule,
     CommonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './modal-empresa.component.html',
  styleUrl: './modal-empresa.component.scss'
})

export class ModalEmpresaComponent implements OnInit{

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
      nome: ['', Validators.required]
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

}
