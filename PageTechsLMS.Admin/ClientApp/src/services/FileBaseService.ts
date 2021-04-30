import {  Client } from '@/apis/API'
import * as qiniu from 'qiniu-js'

export default class FileBaseService {
    // private filebaseSetting = new FileSetting();
    private filebaseClient = new Client();
    baseUrl = location.protocol + "//" + location.host

    get Setting() {
        return (async () => {
            var res = await this.filebaseClient.getSetting()

            // let keys = {
            //     AK: setting?.aliOSSAK ?? setting?.qiniuAK ?? "",
            //     SK: setting?.aliOSSSK ?? setting?.qiniuSK ?? ""
            // }
            return {
                type: res.use || "local",
                url: res.url
                //keys: keys
            }
        })
    }
    public async Upload(name: string, file: Blob): Promise<string> {
        console.log('filebase service upload')
        var setting = (await this.Setting());
        var type = setting.type.toLowerCase();
        var url = setting.url
        console.log(type)
        switch (type) {
            default:
            case "local":
                var res = await this.filebaseClient.uploadFile({ fileName: name, data: file })
                return this.baseUrl + "/Uploads" + res;
            case "alioss":
                break;
            case "qiniu":
                var token = await this.filebaseClient.generateQiniu()
                console.log(token)
                const putExtra = {
                    fname: name,
                };
                qiniu.upload(new File([file], name), name, token, putExtra)
                    .subscribe({
                        next(position) {
                            console.log('Current Position: ', position);
                        },
                        error(msg) {
                            console.log('Error Getting Location: ', msg);
                        },
                        complete(res) {
                            console.log(res)
                            return url + "/" + res.key
                        }
                    })

                break;
        }
        return url + "/" + name;
    }
}