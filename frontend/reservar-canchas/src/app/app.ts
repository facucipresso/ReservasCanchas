import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Header } from './header/header';
import { Footer } from './footer/footer';
import { PrimeNG } from 'primeng/config';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ButtonModule, Header, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'reservar-canchas';
  constructor(private primengConfig: PrimeNG) {
    this.primengConfig.setTranslation({
      firstDayOfWeek: 1,
      dayNames: ["domingo","lunes","martes","miércoles","jueves","viernes","sábado"],
      dayNamesShort: ["dom","lun","mar","mié","jue","vie","sáb"],
      dayNamesMin: ["D","L","M","X","J","V","S"],
      monthNames: ["Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"],
      monthNamesShort: ["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"],
      today: 'Hoy',
      clear: 'Limpiar'
    });
  }
}
