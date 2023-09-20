import { Component, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss']
})
  export class ServerErrorComponent implements OnInit {

    error: any;

    constructor(private router: Router){
      const navigation = this.router.getCurrentNavigation();
      this.error = navigation?.extras?.state?.error;

      if (this.error) {
        const extras: NavigationExtras = {
          state: null // Clear the state
        };
        this.router.navigate([], extras);
      }
    }
    ngOnInit(){
      
    }
  }
