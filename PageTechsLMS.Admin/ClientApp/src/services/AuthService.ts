import { Log, User, UserManager, SigninRequest } from 'oidc-client';
import auth0 from 'auth0-js';
import { request } from 'umi';
import qs from 'qs'
import axios from 'axios';

// const idsConfig = {
//   stsAuthority: 'https://localhost:5001',
//   clientId: 'AdminPanel',
//   clientSecret: 'psshop_secret',
//   clientRoot: location.protocol + "//" + location.host,
//   clientScope: 'openid profile email role MainPortal offline_access',
//   responseType: 'password',
//   apiRoot: location.protocol + "//" + location.host
// }

const idsConfig = {
  stsAuthority: '',
  clientId: 'AdminPanel',
  clientSecret: 'psshop_secret',
  clientRoot: location.protocol + "//" + location.host,
  clientScope: 'openid profile email role MainPortal offline_access',
  responseType: 'password',
  apiRoot: location.protocol + "//" + location.host
}


export default class AuthService {
  public userManager: UserManager;
  tokenEndpoint = `${idsConfig.stsAuthority}/connect/token`;

  constructor() {
  }

  public getUser(): Promise<User | null> {
    return this.userManager.getUser();
  }
  public async loginPwd(username: string, password: string): Promise<void> {
    return axios.post(this.tokenEndpoint, qs.stringify({
      username,
      password,
      grant_type: idsConfig.responseType,
      client_id: idsConfig.clientId,
      client_secret: idsConfig.clientSecret,
      scope: idsConfig.clientScope

    }), {
      headers: { "Content-Type": "application/x-www-form-urlencoded" },
    });
  }

  public async refreshToken(refreshToken: string): Promise<void> {
    return axios.post(this.tokenEndpoint, qs.stringify({
      refresh_token: refreshToken,
      grant_type: 'refresh_token',
      client_id: idsConfig.clientId,
      client_secret: idsConfig.clientSecret,
      // scope: idsConfig.clientScope

    }), {
      headers: { "Content-Type": "application/x-www-form-urlencoded" },
    });
  }
  public login(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  public renewToken(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public logout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }
}
