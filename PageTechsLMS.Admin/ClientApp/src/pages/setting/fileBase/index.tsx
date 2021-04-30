import React, { useEffect, useState } from 'react';
import { Tabs, Form, Radio, Input, Button } from 'antd';
import { PageContainer, FooterToolbar } from '@ant-design/pro-layout';
import DynamicForm from '@/components/DynamicForm/DynamicForm'
import { FilebaseInfo, FilebaseClient, FilebaseSetting } from '@/apis/API'
import { ModelType, ControlType } from '@/components/DynamicForm/DynamicForm.d';

const fbClient = new FilebaseClient()

const filebase: React.FC<{}> = ({ }) => {
    const [formData, setFormData] = useState<FilebaseSetting>();
    const [showForm,setShowForm] = useState<boolean>(false)
    const [form] = Form.useForm();
    const fileds = [
        { name: 'useFilebase' },
        { name: 'qiniuAK' },
        { name: 'qiniuSK' },
        { name: 'aliOSSAK' },
        { name: 'aliOSSSK' },
    ]

    useEffect(() => {
        const getAddOrUpdate = async () => {
            var res = await fbClient.getSetting()
            setFormData(res)
            setShowForm(true)
        }
        getAddOrUpdate()
    }, [])
    const [fileBaseMode, setFileBaseMode] = useState<string>();
    const { TabPane } = Tabs
    const FormItem = Form.Item;

    return ( 
        <PageContainer>
            {showForm && (
                <Form form={form} fields={fileds}  initialValues={formData}>
                <FormItem label="存储模式" key={""} name="useFilebase" >
                    <Radio.Group value={fileBaseMode}  defaultValue={formData?.useFilebase} onChange={(e) => {
                        setFileBaseMode(e.target.value)
                    }}>
                        <Radio.Button value="local">Local</Radio.Button>
                        <Radio.Button value="qiniuyun">七牛云</Radio.Button>
                        <Radio.Button value="alioss">阿里云OSS</Radio.Button>
                    </Radio.Group>
                </FormItem> 
                <FormItem label="Qiniu AK" name="qiniuAK" style={ {display: fileBaseMode == "qiniuyun" ?'block':'none'}}>
                    <Input />
                </FormItem>
                <FormItem label="Qiniu SK" name="qiniuSK" style={ {display: fileBaseMode == "qiniuyun" ?'block':'none'}}>
                    <Input />
                </FormItem> 
                <FormItem label="AliOSS AK" name="aliOSSAK" style={ {display: fileBaseMode == "alioss" ?'block':'none'}}>
                    <Input />
                </FormItem>
                <FormItem label="AliOSS SK" name="aliOSSSK" style={ {display: fileBaseMode == "alioss" ?'block':'none'}}>
                    <Input />
                </FormItem> 
                <FormItem>
                    <Button onClick={async () => {
                        const _formData = form.getFieldsValue();
                        console.log(_formData)
                        await fbClient.setStting(_formData)
                    }} >保存</Button>
                </FormItem>
            </Form>
            )} 
        </PageContainer>)
}

export default filebase