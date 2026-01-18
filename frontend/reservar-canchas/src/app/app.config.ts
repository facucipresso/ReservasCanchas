import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import MyPreset from './mypreset';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es'
import { authInterceptor } from './interceptors/auth-interceptor';
import { MessageService } from 'primeng/api';

registerLocaleData(localeEs);
export const appConfig: ApplicationConfig = {
  providers: [
    MessageService,
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme:{
        preset: MyPreset,
        options: {
          prefix: 'p',
          darkModeSelector: 'none',
        }
      }
    }),
    {provide:LOCALE_ID, useValue:'es'},
    provideHttpClient(
      withInterceptors([authInterceptor])
    )
  ]
};
