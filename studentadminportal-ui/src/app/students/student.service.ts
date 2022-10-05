// this connects to our api

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../models/api-models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseApiUrl = 'https://localhost:44375';

  constructor(private httpClient: HttpClient) { }

  // so that it will return an Observable of type student
  getStudents(): Observable<Student[]> {
    // this is the url of your API route
   return this.httpClient.get<Student[]>(this.baseApiUrl + '/api/students');
  }

  getStudent(studentId: string): Observable<Student> {
    return this.httpClient.get<Student>(this.baseApiUrl + '/api/students/' + studentId);
  }
}
