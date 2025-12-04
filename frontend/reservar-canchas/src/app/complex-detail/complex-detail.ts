import { Component, Input, OnInit } from '@angular/core';
import { ComplexModel } from '../models/complex.model';
import { ActivatedRoute } from '@angular/router';
import { FieldModel } from '../models/field.model';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { FieldType } from '../models/fieldtype.enum';
import { FloorType } from '../models/floortype.enum';
import { ButtonModule } from 'primeng/button';
import { PanelModule } from 'primeng/panel';
import { Message } from 'primeng/message';
import { Select } from 'primeng/select';

@Component({
  selector: 'app-complex-detail',
  imports: [TableModule, CommonModule, ButtonModule, PanelModule, Message, Select],
  templateUrl: './complex-detail.html',
  styleUrl: './complex-detail.css',
})
export class ComplexDetail implements OnInit {
  complex!: any;
  reservations!:any;
  fields: FieldModel[] = [];
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    const complexId = this.route.snapshot.paramMap.get('id');
    console.log(`Id del complejo ${complexId}`);
    this.complex = {
      id: 1,
      name: 'Siempre al 10',
      description:
        'Un espacio diseñado para tu rendimiento y bienestar. Contamos con instalaciones de última generación, piscina climatizada, canchas múltiples y un gimnasio completamente equipado. Más que un centro deportivo, es tu comunidad para superar metas.',
      province: 'Santa Fé',
      locality: 'Rosario',
      street: 'Vera Mújica',
      services: [
        {
          id: 1,
          name: 'WiFi',
        },
        {
          id: 2,
          name: 'Cumpleaños',
        },
        {
          id: 3,
          name: 'Parrilla',
        },
      ],
      averageRating: 4,
      startIlumination: "19:00",
      aditionalIlumination: 15,
      timeSlots: [
        {
          id: 1,
          complexId: 1,
          weekDay: 'Lunes',
          initTime: '10:00',
          endTime: '22:00',
        },
        {
          id: 2,
          complexId: 1,
          weekDay: 'Martes',
          initTime: '10:00',
          endTime: '22:00',
        },
        {
          id: 1,
          complexId: 2,
          weekDay: 'Miercoles',
          initTime: '10:00',
          endTime: '22:00',
        },
        {
          id: 1,
          complexId: 1,
          weekDay: 'Jueves',
          initTime: '10:00',
          endTime: '22:00',
        },
        {
          id: 1,
          complexId: 1,
          weekDay: 'Viernes',
          initTime: '10:00',
          endTime: '22:00',
        },
      ],
      number: '510',
      minPriceHour: 25000,
      imagePath: '/img/complejo.jpg',
    };

    const fieldss = [{
      id: 1,
      complexId: 1,
      name: 'Cancha 1',
      fieldType: FieldType.Cancha11,
      floorType: FloorType.Cemento,
      hourPrice: 25000,
      initTime:'08:00',
      endTime: '02:00',
      ilumination: false,
      covered: true,
      active: true,
    },
    {
      id: 2,
      complexId: 1,
      name: 'Cancha 1',
      fieldType: FieldType.Cancha11,
      floorType: FloorType.Cemento,
      hourPrice: 25000,
      initTime:'08:00',
      endTime: '02:00',
      ilumination: true,
      covered: true,
      active: true,
    },
    {
      id: 3,
      complexId: 1,
      name: 'Cancha 1',
      fieldType: FieldType.Cancha11,
      floorType: FloorType.Cemento,
      hourPrice: 25000,
      initTime:'08:00',
      endTime: '02:00',
      ilumination: false,
      covered: false,
      active: true,
    },
    {
      id: 4,
      complexId: 1,
      name: 'Cancha 1',
      fieldType: FieldType.Cancha11,
      floorType: FloorType.Cemento,
      hourPrice: 25000,
      initTime:'08:00',
      endTime: '02:00',
      ilumination: true,
      covered: false,
      active: true,
    }
  ];

    this.fields = fieldss;
    this.reservations = {
    complexId:1,
    date: "01/05/2025",
    fieldsWithReservedHours:[
      {
        fieldId:1,
        reservedHours:[
          '08:00','09:00','10:00','14:00','15:00','16:00','17:00','18:00','22:00','23:00','00:00'
        ]
      },
      {
        fieldId:2,
        reservedHours:[
          '08:00','09:00','10:00','14:00','15:00','18:00','22:00','23:00','00:00'
        ]
      },
      {
        fieldId:3,
        reservedHours:[
          '08:00','09:00','10:00','14:00','15:00','16:00','17:00','18:00','22:00'
        ]
      },
      {
        fieldId:4,
        reservedHours:[
          '08:00','09:00','10:00','13:00','14:00','15:00','16:00','17:00','18:00','19:00','20:00','21:00','22:00','23:00','00:00'
        ]
      }
    ]
  };
  }
  private generateHourRange(start: string, end: string): string[] {
  const hours: string[] = [];

  let [startH] = start.split(':').map(Number);
  let [endH] = end.split(':').map(Number);

  // si el horario pasa la medianoche (ej: 08:00 → 02:00)
  const passesMidnight = endH < startH;

  while (true) {
    const formatHour = startH.toString().padStart(2, '0') + ':00';
    hours.push(formatHour);

    if (!passesMidnight && startH === endH) break;

    startH = (startH + 1) % 24;

    if (passesMidnight && startH === endH) {
      hours.push(end.padStart(5, '0'));
      break;
    }
  }

  return hours;
  }
  getSelectableHours(fieldId: number, init: string, end: string) {
  const hours = this.generateHourRange(init, end);

  const fieldReservation = this.reservations.fieldsWithReservedHours
    .find((r:any) => r.fieldId === fieldId);

  const reserved = fieldReservation?.reservedHours ?? [];

  return hours.map(h => ({
    hour: h,
    disabled: reserved.includes(h)
  }));
}
}
