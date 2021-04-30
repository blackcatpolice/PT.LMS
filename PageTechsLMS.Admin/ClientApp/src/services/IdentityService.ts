import qs from 'qs'
import axios from 'axios';

export default class IdentityService {
    host = location.protocol+"//"+location.host+`/api`;
    constructor() {
    }

    public async getRoles(): Promise<any> {
        return axios.get(`${this.host}/UserInfo/GetRole`)
    }
    public async getPermission(): Promise<any> {
        return axios.get(`${this.host}/UserInfo/GetPermission`)
    }
    public async ChangePassword(): Promise<any> {
        return axios.get(`${this.host}/UserInfo/ChangePassword`)
    }
}