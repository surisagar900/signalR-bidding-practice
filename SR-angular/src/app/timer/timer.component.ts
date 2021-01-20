import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css'],
})
export class TimerComponent implements OnInit {
  constructor() {}

  date: string;

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    console.log(form.value);

    this.date = formatDate(
      form.value.utcTime,
      'medium',
      'en-IN',
      form.value.timeZone
    );

    console.log(this.date);
  }
}
