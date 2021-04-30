
import { AxiosResponse } from 'axios';
import { notification } from 'antd'

export default class ApiClientBase {
    protected transformOptions(options: any) {
        options.headers.common = {
            "Authorization": "Bearer " + window.localStorage['token']
        };
        
        return Promise.resolve(options);
    }

    protected transformResult(url: string, response: AxiosResponse, processor: (response: AxiosResponse) => any) {
        if (response.status !== 200 && response.status !== 204) {
            console.log('err', response)
            response.data.text().then((errTxt: any) => {
                notification.error({
                    description: '数据请求错误',
                    message: url
                })
            });

        } else {
            return processor(response);
        }
    }
}