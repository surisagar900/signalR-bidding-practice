import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { TimerComponent } from './timer/timer.component';
import { AngularFileUploaderModule } from 'angular-file-uploader';

@NgModule({
  declarations: [AppComponent, TimerComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AngularFileUploaderModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
