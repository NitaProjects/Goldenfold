import {
    AbstractControl,
    ValidationErrors,
    AsyncValidatorFn,
  } from '@angular/forms';
  import { Observable, of } from 'rxjs';
  import { map, catchError } from 'rxjs/operators';
  import { ApiService } from '../services/api.service';
 
 
export const asyncConsultaExistsValidator = (apiService: ApiService): AsyncValidatorFn => {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      const idPaciente = control.value;
  
      if (!idPaciente) {
        return of(null);   // si no hi ha res escrit no fer validació
      }
  
      return apiService.getConsultas(idPaciente).pipe(
        map((consultas) => {
          return consultas.length > 0 ? null : { consultaNotFound: true };
        }),
        catchError(() => of({ consultaNotFound: true }))
      );
    };
  };
  

  