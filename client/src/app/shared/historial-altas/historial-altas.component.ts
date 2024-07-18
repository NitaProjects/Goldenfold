import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-historial-altas',
  templateUrl: './historial-altas.component.html',
  styleUrls: ['./historial-altas.component.css']
})
export class HistorialAltasComponent implements OnInit {
  pacientes = [
    { idHistorial: 1, idPaciente: 1, fechaAlta: new Date('2024-01-05T00:00:00'), diagnostico: 'Hipertensión', tratamiento: 'Enalapril 10 mg diarios' },
    { idHistorial: 2, idPaciente: 2, fechaAlta: new Date('2024-01-12T00:00:00'), diagnostico: 'Diabetes tipo 2', tratamiento: 'Metformina 500 mg dos veces al día' },
    { idHistorial: 3, idPaciente: 3, fechaAlta: new Date('2024-01-18T00:00:00'), diagnostico: 'Asma', tratamiento: 'Salbutamol inhalador según necesidad' },
    { idHistorial: 4, idPaciente: 4, fechaAlta: new Date('2024-01-25T00:00:00'), diagnostico: 'Hipotiroidismo', tratamiento: 'Levotiroxina 50 mcg diarios' },
    { idHistorial: 5, idPaciente: 5, fechaAlta: new Date('2024-02-02T00:00:00'), diagnostico: 'Artritis reumatoide', tratamiento: 'Metotrexato 15 mg semanal' },
    { idHistorial: 6, idPaciente: 6, fechaAlta: new Date('2024-02-10T00:00:00'), diagnostico: 'Depresión mayor', tratamiento: 'Fluoxetina 20 mg diarios' },
    { idHistorial: 7, idPaciente: 7, fechaAlta: new Date('2024-02-18T00:00:00'), diagnostico: 'Insuficiencia cardíaca', tratamiento: 'Furosemida 40 mg diarios' },
    { idHistorial: 8, idPaciente: 8, fechaAlta: new Date('2024-02-25T00:00:00'), diagnostico: 'Osteoporosis', tratamiento: 'Alendronato 70 mg semanal' },
    { idHistorial: 9, idPaciente: 9, fechaAlta: new Date('2024-03-03T00:00:00'), diagnostico: 'EPOC', tratamiento: 'Tiotropio inhalador una vez al día' },
    { idHistorial: 10, idPaciente: 10, fechaAlta: new Date('2024-03-10T00:00:00'), diagnostico: 'Migraña', tratamiento: 'Sumatriptán 50 mg según necesidad' }
  ];

  constructor() { }

  ngOnInit(): void {
  }

  editRecord(id: number) {
    console.log(`Edit record with ID ${id}`);
  }

  copyRecord(id: number) {
    console.log(`Copy record with ID ${id}`);
  }

  deleteRecord(id: number) {
    console.log(`Delete record with ID ${id}`);
  }
}

