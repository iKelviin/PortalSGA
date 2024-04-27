// Função validadora para números de telefone
import { AbstractControl, ValidatorFn } from "@angular/forms";

export function cepValidator(): ValidatorFn {
  return (control: AbstractControl) => {
    if (control.value) {
      const cepPattern = /^[0-9]*$/;
      if (!cepPattern.test(control.value)) {
        return { 'invalidCepPattern': true };
      }
    }
    return null;
  };
}


