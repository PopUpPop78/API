import { CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const adminRoleGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);

  if(auth.getRoles().includes('Admin'))
  {
    return true;
  }

  return false;
};
