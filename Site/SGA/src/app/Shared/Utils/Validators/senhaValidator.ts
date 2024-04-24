import { AbstractControl, FormGroup, ValidatorFn } from "@angular/forms";
import zxcvbn, { ZXCVBNResult } from 'zxcvbn';


export function senhaValidator(): ValidatorFn {
  return (control: AbstractControl) => {

    const result: ZXCVBNResult = zxcvbn(control.value);

    const PASSWORD_IS_WEAK_OR_MEDIUM = result.score !== 4;

    if (PASSWORD_IS_WEAK_OR_MEDIUM) {
      return { 'invalidPasswordStrength': true };
    }


    return null;
  }
}

export function confirmeSenhaValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const formGroup = control as FormGroup;

    const senhaControl = formGroup.get('senha');
    const confirmSenhaControl = formGroup.get('confirmSenha');

    if (!senhaControl || !confirmSenhaControl) {
      return null;
    }

    const senha = senhaControl.value;
    const confirmSenha = confirmSenhaControl.value;

    if (senha !== confirmSenha) {
      confirmSenhaControl.setErrors({ 'invalidPasswordConfirmation': true });
      return { 'invalidPasswordConfirmation': true };
    } else {
      confirmSenhaControl.setErrors(null); // Limpar erros se as senhas forem iguais
      return null;
    }
  };
}

