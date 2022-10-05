import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, TitleStrategy } from '@angular/router';
import { Student } from 'src/app/models/ui-models/studentui.model';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {
  studentId: string | null | undefined;
  // from UI models
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    profileImageUrl: '',
    gender: {
      id: '',
      description: ''
    },
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: ''
    }
  }

  // si activated route yung kukuha ng parameters na galing sa route??
  constructor(private readonly studentService: StudentService, private readonly route: ActivatedRoute) { }

  // paramMap is to read the route??
  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        // same name sa id na nasa app-routing module tapos i-assign mo yung makukuhang id sa studentId property na ginawa mo sa taas
        this.studentId = params.get('id');

        if(this.studentId) {
          this.studentService.getStudent(this.studentId).subscribe(
            (successResponse) => {
              // para magamit mo sa html file
              this.student = successResponse;
            },
            (errorResponse) => {
              console.log(errorResponse);
            }
          );
        }
      }
    );
  }
}