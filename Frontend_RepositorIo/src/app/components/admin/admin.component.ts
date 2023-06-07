import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { MatDialog } from '@angular/material/dialog';
import { WarningModalComponent } from 'src/app/shared/components/warningModal/warning-modal.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  public user: any;
  public displayedColumns: string[] = ['id', 'username', 'fechaAlta', 'rol', 'accion'];
  public userList = [];

  constructor(
    private _loginService: LoginService,
    private dialog: MatDialog
  ) {}
  
  public ngOnInit(): void {
    this.user = this._loginService.user;
    this._loginService.getUsers()
      .subscribe(
        res => {
          this.userList = [...[res]].flat();
          console.log(this.userList)
        }
      )
  }

  handlePageEvent(event: any) {

  }

  onDelete(user: any) {
    const dialogRef = this.dialog.open(WarningModalComponent, {
      maxWidth: '800px',
      data: {
        title: ` usuario`,
        message: `Está a punto de eliminar el usuario ${user.username}`,
        isGeneralPurposeModal: false,
      },
    });

    dialogRef.afterClosed().subscribe((res) => {
      if (res) {
        this._loginService.deleteUser(user)
          .subscribe(
            res => {
              console.log(res)
            },
            err => {
              console.log(err)
            }
          )
      }
    });
  }
}
