import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-tessssssst',
  templateUrl: './tessssssst.component.html',
  styleUrls: ['./tessssssst.component.css']
})

export class TessssssstComponent implements OnInit {

  inputData: string = '';

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.inputData = params['data'];
    });
  }
}
