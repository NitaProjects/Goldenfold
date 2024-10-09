import {
    AbstractControl,
    ValidationErrors,
    AsyncValidatorFn,
  } from '@angular/forms';
  import { Observable, of } from 'rxjs';
  import { map, catchError, switchMap, debounceTime } from 'rxjs/operators';
  import { ApiService } from '../services/api.service';
  
  export const asyncPatientIdExistsValidator = (
    apiService: ApiService
  ): AsyncValidatorFn => {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      // si no hi ha res escrit no fer validació
      const idPaciente = control.value;
      if (!idPaciente) {
        return of(null);
      }
  
      return of(idPaciente).pipe(
        debounceTime(300), 
        switchMap((idPaciente) =>
          apiService.getPacienteById(idPaciente).pipe(
            map((paciente) => {
              if (paciente) {
                return null; //pacient existeix
              } else {
                return { patientIdNotFound: true }; 
              }
            }),
            catchError(() => of({ patientIdNotFound: true }))
          )
        )
      );
    };
  };
  