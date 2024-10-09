// validators/rol-name-exists.validator.ts
import {
  AbstractControl,
  ValidationErrors,
  AsyncValidatorFn,
} from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map, catchError, switchMap, debounceTime } from 'rxjs/operators';
import { ApiService } from '../services/api.service';

export const asyncRolNameExistsValidator = (
  apiService: ApiService
): AsyncValidatorFn => {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    return of(control.value).pipe(
      debounceTime(300),
      switchMap((value) =>
        apiService.getRoles(value).pipe(
          map((roles) => {
            // Filtrar los roles en el frontend
            const rolesFiltrados = roles.filter(
              (rol) => rol.NombreRol.toLowerCase().trim() === value.toLowerCase().trim()
            );

            if (rolesFiltrados.length > 0) {
              return { asyncFieldExisting: true };
            }
            return null;
          }),
          catchError(() => of(null))
        )
      )
    );
  };
};

