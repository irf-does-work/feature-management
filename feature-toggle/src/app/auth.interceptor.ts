import { HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { TOKEN_KEY } from './shared/constants';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  const authToken = localStorage.getItem(TOKEN_KEY);

  // console.log("console from auth interceptor\n" + `Bearer ${authToken}`);

  if (!authToken) {
    return next(req);
  }

  const newRq = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${authToken}`),
  });

  return next(newRq);
};
