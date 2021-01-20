import { Component, OnInit, ViewChild } from '@angular/core';
import { UserBidService } from './user-bid.service';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { bid } from './models/Bid';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  @ViewChild('f') form: NgForm;

  constructor(private bidService: UserBidService) {}

  bid: number;
  highestBid: bid;

  ngOnInit() {
    this.bidService.buildConnection();

    this.bidService.highestBid.subscribe((res) => {
      console.error(res);
      this.highestBid = res;
    });
  }

  onSubmit() {
    let val = this.form.value;
    this.bidService.sendBid(val.username, val.amount).then(() => {
      this.form.reset();
    });
  }
}
