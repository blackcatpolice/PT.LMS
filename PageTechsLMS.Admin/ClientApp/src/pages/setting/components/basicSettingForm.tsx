import React, { useState, useEffect } from 'react'
import { PageContainer } from '@ant-design/pro-layout'
import { Form, Input, Button } from 'antd';
import { InfoCircleOutlined } from '@ant-design/icons';
import { Client } from '@/apis/API'

type RequiredMark = boolean | 'optional';

const { TextArea } = Input;

const page: React.FC<{}> = (props: any) => {
    const [form] = Form.useForm();
    var client = new Client(); 
    console.log('get setting')
    useEffect(() => {
        getSetting();
    }, [])

    const saveSetting = (value: any) => {
        console.log(value)
        client.postSiteSetting(value)
    }

    const getSetting = async () => {
        var apiMsg = await client.getSiteSetting()
        form.setFieldsValue (apiMsg.data)
        //setSiteSetting(apiMsg.data);
        console.log(apiMsg.data)
    }

    return (
        <Form
            form={form}
            layout="vertical"
            //initialValues={siteSetting}
            requiredMark={"optional"}
            onFinish={saveSetting}
        >
            <Form.Item name={'id'} style={{ display: 'none' }}>

            </Form.Item>
            <Form.Item label="站点名" tooltip="站点的名称" name="siteName">
                <Input placeholder="Site name" />
            </Form.Item>
            <Form.Item
                label="关键字"
                tooltip="SEO 关键字"
                name="keys"
            >
                <Input placeholder="" />
            </Form.Item>
            <Form.Item
                label="描述"
                tooltip="SEO 描述"
                name="description"
            >
                <Input placeholder="" />
            </Form.Item>
            <Form.Item
                label="站点信息"
                tooltip="站长之家 ，广告外联等信息脚本"
                name="footerInfo"
            >
                <TextArea placeholder="" />
            </Form.Item>
            <Form.Item
                label="站点信息脚本"
                tooltip="站长之家 ，广告外联等信息脚本"
                name="footerScript"
            >
                <TextArea placeholder="" />
            </Form.Item>
            <Form.Item>
                <Button type="primary" htmlType="submit">保存</Button>
            </Form.Item>
        </Form>
    );
}

export default page;