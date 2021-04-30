import API from "./baseApi";
import { useModel } from 'umi';

export default class UserService { 
  /**
   * getUserInfo
   */
  public async getUserInfo() {
    return API.get('/userinfo/getuserinfo')
  }

  public async setUserInfo(data: any) {
    return API.post('/userinfo/setuserinfo', data)
  }
}