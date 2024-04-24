// Função validadora para números de telefone
import { AbstractControl, ValidatorFn } from "@angular/forms";

export function telefoneValidator(): ValidatorFn {
  return (control: AbstractControl) => {
    if (control.value) {
      const telefonePattern = /^[0-9]*$/;
      if (!telefonePattern.test(control.value)) {
        return { 'invalidTelefonePattern': true };
      }
    }
    return null;
  };
}


