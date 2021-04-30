import React, { useState, useEffect } from 'react';
import { Form, Input, Button } from 'antd';
import { Client } from '@/apis/API'

const page: React.FC<{}> = () => {
    const [form] = Form.useForm();
    var client = new Client();
    console.log('get setting')
    useEffect(() => {
        getSetting();
    }, [])

    const getSetting = async () => {
        var configData = await client.getConfigSetting()
        // configData = configData.replace(/\/\/[\S\s]+?\n/g,'')
         form.setFieldsValue(configData) 
        console.log(configData)
    }
    return (
        <Form
            form={form}
            layout="vertical"
        //initialValues={siteSetting} 
        >
            <Form.Item label="WxMP.AppKey" tooltip={'WxMP.AppKey'} name={['WxMP', 'AppKey']} >
                <Input disabled={true} />
            </Form.Item>
            <Form.Item label="WxMP.AppSecret" tooltip={["WxMP", "AppSecret"]} name={["WxMP", "AppSecret"]}>
                <Input disabled={true} />
            </Form.Item>

            <Form.Item label="File.Use" tooltip={"File.Use"} name={["File", "Use"]}>
                <Input disabled={true} />
            </Form.Item>
            <Form.Item label="File.AppKey" tooltip={"File.AppKey"} name={["File", "AppKey"]}>
                <Input disabled={true} />
            </Form.Item>
            <Form.Item label="File.AppSecret" tooltip={"File.AppSecret"} name={["File", "AppSecret"]} >
                <Input disabled={true} />
            </Form.Item>
        </Form>
    );
}

export default page