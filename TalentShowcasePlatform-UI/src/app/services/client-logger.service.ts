import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class ClientLogger {
  private isBrowser: boolean;

  constructor(@Inject(PLATFORM_ID) platformId: Object) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  log(...args: any[]) {
    if (this.isBrowser) {
      console.log(...args);
    }
  }

  warn(...args: any[]) {
    if (this.isBrowser) {
      console.warn(...args);
    }
  }

  error(...args: any[]) {
    if (this.isBrowser) {
      console.error(...args);
    }
  }
}
