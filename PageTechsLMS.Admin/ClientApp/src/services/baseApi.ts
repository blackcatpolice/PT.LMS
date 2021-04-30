import qs from 'qs'
import axios from 'axios';  
const baseHost = location.protocol+"//"+location.host;
export default class API {
    public static async get(url: string) {
        return axios.get(baseHost+url, {
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
        });
    }

    public static async post( url: string, data: string) {
        return axios.post(baseHost+url, qs.stringify(data), {
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
        });
    }
}
