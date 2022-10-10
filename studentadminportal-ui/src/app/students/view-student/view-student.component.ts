import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router, TitleStrategy } from '@angular/router';
import { Gender } from 'src/app/models/ui-models/genderui.model';
import { Student } from 'src/app/models/ui-models/studentui.model';
import { GenderService } from 'src/app/services/gender.service';
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
  isNewStudent = false;
  header = '';
  genderList: Gender[] = []

  // si activated route yung kukuha ng parameters na galing sa route??
  constructor(private readonly studentService: StudentService, private readonly route: ActivatedRoute, private readonly genderService: GenderService, private snackbar: MatSnackBar, private router: Router) { }

  // paramMap is to read the route??
  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        // same name sa id na nasa app-routing module tapos i-assign mo yung makukuhang id sa studentId property na ginawa mo sa taas
        this.studentId = params.get('id');

        if(this.studentId) {
          // check if route contains keyword 'Add' para ma-determine kung mag-add siya or mag-update
          if(this.studentId.toLowerCase() === 'Add'.toLowerCase()) {
            // -> new Student Functionality
            this.isNewStudent = true;
            this.header = 'Add New Student';

          } else {
            // -> Existing Student Functionality
            this.isNewStudent = false;
            this.header = 'Edit Student';

            this.studentService.getStudent(this.studentId).subscribe(
              (successResponse) => {
                // para magamit mo sa html file
                this.student = successResponse;
              }
            );
          }

          // since need ng both add and update yung gender, pwedeng nasa labas lang siya
          this.genderService.getGenderList().subscribe(
            successResponse => {
              this.genderList = successResponse;
            }
          );
        }
      }
    );
  }

  onUpdate(): void {
    // Call Student Service to Update Student
    this.studentService.updateStudent(this.student.id, this.student).subscribe(
      (successResponse) => {
        // Show a notification
        this.snackbar.open('Changes have been saved', undefined, {
          duration: 2000
        });
      },
      (errorResponse) => {
        // Log it
      }
    );
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id).subscribe(
      (successResponse) => {
        this.router.navigateByUrl('students').then(() => {
          this.snackbar.open('Record has been deleted', undefined, {
            duration: 2000
          });
      })
      },
      (errorResponse) => {
        // Log it
      }
    )
  }

  onAdd(): void {
    this.studentService.addStudent(this.student).subscribe(
      (successResponse) => {
        // if you want to redirect it to the student detail, use `students/${successRespose.id}` as URL take note of ``
        this.router.navigateByUrl('students').then(() => {
          this.snackbar.open('Record has been added', undefined, {
            duration: 2000
          });
        })
      },
      (errorResponse) => {
        // Log error
      }
    )
  }
}
