export interface User  {
    firstName:string,
    lastName:string,
    email:string,
    address:Address,
    token:string,
    refreshToken:string
}

export interface Address{
    line1:string,
    line2?:string,
    city:string,
    state:string,
    country:string,
    postalCode:string
}

export interface AuthDto {
  userName: string;
  email: string;
  token: string;
  refreshToken: string;
  refreshTokenExpiration: Date; 
  userId: string;
  roles: string[];
  isAuthenticated: boolean;
}