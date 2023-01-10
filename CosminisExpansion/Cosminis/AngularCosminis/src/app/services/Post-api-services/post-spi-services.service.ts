import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Posts } from '../../Models/Posts';

@Injectable({
  providedIn: 'root'
})
export class PostSpiServicesService {
  url : string = environment.api;

  constructor(private http: HttpClient) { }

  getPostsByUserId(ID : number) : Observable<Posts[]> {
    return this.http.get(this.url + `postsBy/${ID}`) as Observable<Posts[]>;  
  }

  getAllFriendsPosts(username : string) : Observable<Posts[]> {
    return this.http.get(this.url + `viewFriendsPosts?username=${username}`) as Observable<Posts[]>;
  }   

  SubmitPostResourceGen(content: string, postersId : number) : Observable<Posts> {
    return this.http.post(this.url + `submitPost?Content=${content}&PosterID=${postersId}`, content) as Observable<Posts>; 
  } 
}
