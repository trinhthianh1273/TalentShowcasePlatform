import { Component, Input, input } from '@angular/core';
import { GroupPostModel } from '../../../models/group-post-model';
import { SharedModule } from '../../../shared/shared.module';
import { Router } from '@angular/router';
import { Enviroment } from '../../../../environment';

@Component({
  selector: 'app-post-cart',
  imports: [
    SharedModule
  ],
  templateUrl: './post-cart.component.html',
  styleUrl: './post-cart.component.css'
})
export class PostCartComponent {
  @Input() PostData! : GroupPostModel;
  groupPostPath = Enviroment.groupPostPath;
  
  constructor(
    private router: Router
  ) { }

  ngOnInit(): void { 
    console.log(this.groupPostPath);
  }

  gotoPost(id: string) {
    this.router.navigate([`/community/post/${id}`])
  }
}
