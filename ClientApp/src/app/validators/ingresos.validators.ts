import { AbstractControl, ValidationErrors, ValidatorFn, AsyncValidatorFn, FormControl } from '@angular/forms';
import { catchError, debounceTime, delay, map, Observable, of, switchMap } from 'rxjs';
import { ApiService } from '../services/api.service';

export class IngresosValidators {

  static noWhitespaceValidator(): ValidatorFn {
      return (control: AbstractControl): ValidationErrors | null => {
        const isWhitespace = (control.value || '').trim().length === 0;
        const isValid = !isWhitespace;
        return isValid ? null : { whitespace: true };
      };
  }


}
