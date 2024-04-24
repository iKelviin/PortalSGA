
// Função validadora para números de telefone
import { AbstractControl, ValidatorFn } from "@angular/forms";

export function emailValidator(): ValidatorFn {
  return (control: AbstractControl) => {

    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+[a-zA-Z]{2,}$/;

    const isValid = emailPattern.test(control.value);

    return isValid ? null : {'invalidEmailPattern': true};

  }
}


