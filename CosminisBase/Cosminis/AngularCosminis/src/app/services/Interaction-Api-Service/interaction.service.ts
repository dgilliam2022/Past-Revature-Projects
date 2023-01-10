import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cosminis } from '../../Models/Cosminis';

@Injectable({
  providedIn: 'root'
})
export class InteractionService {
  url : string = environment.api;  

  constructor(private http: HttpClient) { }

  SetShowcaseCompanion(UserId : number, CompanionId : number) : Observable<Cosminis> {
    return this.http.put(this.url + `setCompanion?userId=${UserId}&companionId=${CompanionId}`, UserId) as Observable<Cosminis>;
  } 
}