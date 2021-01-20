import { Injectable } from '@angular/core';
import { bid } from './models/Bid';
import { BehaviorSubject } from 'rxjs';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class UserBidService {
  highestBid = new BehaviorSubject<bid>(null);

  private hubConnection: HubConnection;

  constructor() {}

  public buildConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/bidHub')
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.warn('connection established');
      })
      .then(() => this.getInitialBid())
      .then(() => this.getHighestBidSend())
      .catch((err) => {
        console.error('error while connecting .. ' + err);
      });
  }

  public sendBid(userName: string, amount: number): Promise<any> {
    console.log('sending bid');
    let model = {
      username: userName,
      amount: amount,
    };
    return this.hubConnection.invoke('BidRecieve', model);
  }

  private getInitialBid() {
    this.hubConnection.invoke('initialBidStatus').then((data) => {
      this.highestBid.next(data);
    });
  }

  private getHighestBidSend() {
    this.hubConnection.on('HighestBidSend', (data) => {
      this.highestBid.next(data);
    });
  }
}
