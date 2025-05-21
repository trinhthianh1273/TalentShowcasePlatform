import { Component, Input, input } from '@angular/core';
import { GroupPostModel } from '../../../models/group-post-model';

@Component({
  selector: 'app-post-cart',
  imports: [],
  templateUrl: './post-cart.component.html',
  styleUrl: './post-cart.component.css'
})
export class PostCartComponent {
  @Input() PostData! : GroupPostModel;

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    console.log(this.PostData);
  }
}
