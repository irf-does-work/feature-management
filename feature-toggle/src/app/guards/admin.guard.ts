import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  let isAdmin : number;
  const payload = authService.decodeToken()

  payload.IsAdmin === "True" ? isAdmin = 1 : isAdmin = 0;

  console.log()
  if (isAdmin) {

    return true;
    
  } else {
    router.navigate(['/home'])
    return false;
  }
  
};
