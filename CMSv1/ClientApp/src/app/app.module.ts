import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ContactComponent } from './Pages/Contacts/contact/contact.component';
import { LoginComponent } from './Pages/Login/login/login.component';
import { GameDetailsComponent } from './Pages/Games/game-details/game-details.component';
import { GamesComponent } from './Pages/Games/games/games.component';
import { CreateGameComponent } from './Pages/Games/create-game/create-game.component';
import { PromotionsComponent } from './Pages/Promotions/promotions/promotions.component';
import { CreatePromotionComponent } from './Pages/Promotions/create-promotion/create-promotion.component';
import { PromotionDetailComponent } from './Pages/Promotions/promotion-detail/promotion-detail.component';
import { UserComponent } from './Pages/Users/user/user.component';

const AppRoutes: Routes = [
  {
    path: 'User',
    component: UserComponent,
    pathMatch: 'full',
    //canActivate: [AuthGuard]
  },
  {
    path: 'Promotions',
    //canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: PromotionsComponent,
        pathMatch: 'full',
      },
      {
        path: 'Create',
        component: CreatePromotionComponent,
        pathMatch: 'full'
      },
      {
        path: 'Detail/:id',
        component: PromotionDetailComponent,
        pathMatch: 'full'
      }
    ]
  },
  {
    path: 'Games',
    //canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: GamesComponent,
        pathMatch: 'full',
      },
      {
        path: 'Create',
        component: CreateGameComponent,
        pathMatch: 'full'
      },
      {
        path: 'Detail/:id',
        component: GameDetailsComponent,
        pathMatch: 'full'
      }
    ]
  },
  {
    path: 'Contacts',
    component: ContactComponent,
    pathMatch: 'full',
    //canActivate: [AuthGuard]
  },
  {
    path: 'Login',
    component: LoginComponent,
    pathMatch: 'full'
  },
  {
    path: '',
    redirectTo: '/User',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: '/'
  }
]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ContactComponent,
    LoginComponent,
    GameDetailsComponent,
    GamesComponent,
    CreateGameComponent,
    PromotionsComponent,
    CreatePromotionComponent,
    PromotionDetailComponent,
    UserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(AppRoutes)
    //RouterModule.forRoot(
    //  AppRoutes,
    //  { enableTracing: false } // <-- debugging purposes only
    //)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
