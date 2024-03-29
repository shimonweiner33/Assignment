import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { PostListComponent } from './components/post-list/post-list.component';
import { PostComponent } from './components/post/post.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AuthenticationService } from './services/authentication.service';
import { HubsService } from './services/hubs.service';
import { PostsService } from './services/post-display.service';
import { RoomsService } from './services/rooms.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PostComponent,
    PostListComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      // { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: '', redirectTo: 'app', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      // { path: 'post-list:roomNum', component: PostListComponent },
      {path:"post-list/:roomNum", component:PostListComponent},
    ]),
    AppRoutingModule
  ],
  providers: [AuthenticationService, RoomsService, HubsService, PostsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
