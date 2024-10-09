import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService, HistorialAlta, Paciente } from '../../services/api.service';
import { asyncPatientIdExistsValidator } from '../../validators/patientIdExistsValidator';
import { CustomValidators } from '../../validators';
import { asyncConsultaExistsValidator } from '../../validators/consultaExistsValidator';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-historial-altas',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './historial-altas.component.html',
  styleUrls: ['./historial-altas.component.css']
})
export class HistorialAltasComponent implements OnInit {
  historialAltas: HistorialAlta[] = [];
  nuevoHistorialAlta: HistorialAlta = this.inicializarHistorialAlta();
  historialAltaParaActualizar: HistorialAlta | null = null;
  
  historialAltaForm!: FormGroup;
  filtro: string = ''; 
  fechaAltaFiltro: string | undefined;
  notificacion: string | null = null;

  columnaOrdenada: keyof HistorialAlta | null = null;
  orden: 'asc' | 'desc' = 'asc';

  historialSeleccionado: HistorialAlta | null = null;
  mostrarFormularioActualizar: boolean = false;
  numSSFiltro: string = "";
  pacientes: Paciente[] = []; 

  constructor(private apiService: ApiService, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.obtenerHistorialAltas();
    this.obtenerPacientes();
    this.crearFormularioHistorialAlta();
    this.configurarValidaciones();
  }

  inicializarHistorialAlta(): HistorialAlta {
    return {
      IdHistorial: 0,
      IdPaciente: 0,
      IdMedico: 1,
      FechaAlta: new Date(),
      Diagnostico: '',
      Tratamiento: ''
    };
  }

  crearFormularioHistorialAlta(): void {
    this.historialAltaForm = new FormGroup({
      IdPaciente: new FormControl('', {
        validators: [Validators.required],
        asyncValidators: [
          asyncPatientIdExistsValidator(this.apiService), 
          asyncConsultaExistsValidator(this.apiService)
        ],
        updateOn: 'blur' 
      }),
      FechaAlta: new FormControl('', [
        Validators.required,
        CustomValidators.noWhitespaceValidator()
      ]),
      Diagnostico: new FormControl('', [
        Validators.required,
        CustomValidators.noWhitespaceValidator()
      ]),
      Tratamiento: new FormControl('', [
        Validators.required,
        CustomValidators.noWhitespaceValidator()
      ])
    });
  }

  configurarValidaciones(): void {
    if (!this.historialAltaParaActualizar) {
      this.historialAltaForm.get('IdPaciente')?.setAsyncValidators([
        asyncPatientIdExistsValidator(this.apiService), 
        asyncConsultaExistsValidator(this.apiService)
      ]);
    } else {
      this.historialAltaForm.get('IdPaciente')?.clearAsyncValidators();
    }
    this.historialAltaForm.get('IdPaciente')?.updateValueAndValidity();
  }

  obtenerPacientes() {
    this.apiService.getPacientes().subscribe({
      next: (data: Paciente[]) => {
        this.pacientes = data;
        console.log('Pacientes:', this.pacientes);
      },
      error: (error: any) => {
        console.error('Error al obtener pacientes', error);
      }
    });
  }

  filtrarPorFecha() {
    if (this.fechaAltaFiltro) {
      this.historialAltas = this.historialAltas.filter(historial => {
        // Convertir la fecha del historial a formato YYYY-MM-DD
        const fechaHistorial = new Date(historial.FechaAlta).toISOString().slice(0, 10);
        return fechaHistorial === this.fechaAltaFiltro;
      });
    } 
  }

  filtrarPorNumeroSS(): void {
    const filtroSS = this.numSSFiltro.trim().toLowerCase(); 

    if (filtroSS.length > 0) {
      this.historialAltas = this.historialAltas.filter(historial => {
        const paciente = this.getPacienteById(historial.IdPaciente);
        return paciente && paciente.SeguridadSocial && 
               paciente.SeguridadSocial.toLowerCase().includes(filtroSS); 
      }); 
      console.log(this.historialAltas);
    } 

  }

  getPacienteById(idPaciente: number): Paciente | undefined {
    return this.pacientes.find(paciente => paciente.IdPaciente === idPaciente);
  }

  obtenerHistorialAltas(): void {
    this.apiService.getHistorialAltas().subscribe({
      next: (data: HistorialAlta[]) => {
        this.historialAltas = data;
        this.filtrarPorFecha();
      },
      error: (error: any) => {
        console.error('Error al obtener el historial de altas', error);
      }
    });
  }

  onFechaAltaFiltroChange(event: any) {
    this.fechaAltaFiltro = event.target.value; 
    this.filtrarPorFecha();
  }

  agregarHistorialAlta(): void {
    if (this.historialAltaForm.valid) {
      this.nuevoHistorialAlta = {
        ...this.nuevoHistorialAlta,
        IdPaciente: this.historialAltaForm.get('IdPaciente')?.value,
        FechaAlta: this.historialAltaForm.get('FechaAlta')?.value,
        Diagnostico: this.historialAltaForm.get('Diagnostico')?.value,
        Tratamiento: this.historialAltaForm.get('Tratamiento')?.value
      };

      this.apiService.addHistorialAlta(this.nuevoHistorialAlta).subscribe({
        next: (nuevoHistorialAlta: HistorialAlta) => {
          this.historialAltas.push(nuevoHistorialAlta);
          this.filtrarPorFecha(); 
          this.nuevoHistorialAlta = this.inicializarHistorialAlta();
          this.historialAltaForm.reset();
          this.cd.detectChanges();
        },
        error: (error: any) => {
          console.error('Error al agregar el historial de alta', error);
        }
      });
    } else {
      console.warn('El formulario es inválido.');
    }
  }

  toggleActualizarHistorialAlta(historialAlta: HistorialAlta): void {
    if (this.historialAltaParaActualizar?.IdHistorial === historialAlta.IdHistorial) {
      this.historialAltaParaActualizar = null;
      this.historialAltaForm.reset();
    } else {
      this.historialAltaParaActualizar = { ...historialAlta };
      this.configurarValidaciones();
      this.historialAltaForm.patchValue(this.historialAltaParaActualizar);
    }
  }

  actualizarHistorialAlta(): void {
    if (this.historialAltaParaActualizar) {
      this.apiService.updateHistorialAlta(this.historialAltaParaActualizar).subscribe({
        next: (historialAltaActualizado: HistorialAlta) => {
          this.obtenerHistorialAltas();
          this.historialAltaParaActualizar = null;
          this.mostrarFormularioActualizar = false;
          this.notificacion = "Historial Alta actualizado con éxito";
          this.cd.detectChanges();
          this.historialAltaForm.reset();
        },
        error: (error: any) => {
          console.error('Error al actualizar el historial de alta', error);
        }
      });
    }
  }

  borrarHistorialAlta(id: number): void {
    this.apiService.deleteHistorialAlta(id).subscribe({
      next: () => {
        this.historialAltas = this.historialAltas.filter(historialAlta => historialAlta.IdHistorial !== id);
        this.notificacion = "Historial Alta eliminado con éxito.";
        this.cd.detectChanges();
      },
      error: (error: any) => {
        console.error('Error al borrar el historial de alta', error);
      }
    });
  }

  ordenar(columna: keyof HistorialAlta): void {
    this.columnaOrdenada = this.columnaOrdenada === columna ? columna : columna;
    this.orden = this.columnaOrdenada === columna && this.orden === 'asc' ? 'desc' : 'asc';
  }

  aplicarFiltro(filtro: string): void {
    this.filtro = filtro;
    // Aplicar filtros si es necesario
  }

  cerrarPopup(): void {
    this.historialSeleccionado = null;
  }
}
