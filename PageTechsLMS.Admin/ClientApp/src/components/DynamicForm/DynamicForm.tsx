import React, { useState, useEffect } from 'react';
import { Modal, Form, Input, InputNumber, Button, DatePicker, Upload, Tree, Select, Checkbox, Switch, Radio } from 'antd';
import { UploadOutlined, LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import { PageContainer, FooterToolbar } from '@ant-design/pro-layout';
import { ModelType, ControlType } from './DynamicForm.d';
import 'braft-editor/dist/index.css'
import BraftEditor, { EditorState } from 'braft-editor'
import BraftEditorWrapper from '../BraftEditor/BraftEditorWrapper'
import { Player } from 'video-react';
import FilebaseService from '@/services/FileBaseService'
import moment from 'moment'
import CSS from 'csstype';
import AceEditor from "react-ace";
import "ace-builds/src-noconflict/mode-html";
import "ace-builds/src-noconflict/theme-github";
import styles from './DynamicForm.less'
import Reflv from 'reflv';
//import 'video-react/dist/video-react.css';
//import { Option } from 'antd/lib/mentions';

const formLayout = {
    labelCol: { span: 7 },
    wrapperCol: { span: 13 },
};
const { Option } = Select;

const DynamicForm: React.FC<{
    onCancel?: () => void,
    onSubmit: (values: any) => void,
    longIdKey?: string,
    formModelVisible?: boolean,
    modelTypes: ModelType[],
    modelData?: any,
    data?: any,
    useModal?: boolean
}> = (props) => {
    const [form] = Form.useForm();
    const FormItem = Form.Item;
    const { data, longIdKey, useModal, modelTypes, modelData, formModelVisible, onSubmit, onCancel } = props
    const fileds: any = [];
    const [editorState, setEditorState] = useState<EditorState>();
    const [loading, setLoading] = useState<{}>({});
    const [imgUrl, setImgUrl] = useState<{}>({});
    const [videoUrl, setVideoUrl] = useState<{}>({});


    modelTypes.map((item: any) => {
        fileds.push({
            name: [item.propName]
        })
    })

    const redenrModelItem = (modelTypeItem: ModelType, modelData?: any) => {
        switch (modelTypeItem.controlType) {

            case ControlType.Tags:
                const selectChange = (content: string[]) => {
                    console.log(content)
                    var valueObj = {};
                    valueObj[modelTypeItem.propName] = content//.join(',')
                    form.setFieldsValue(valueObj)
                }
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <Select mode="tags" style={{ width: '100%' }} placeholder="Tags Mode" onChange={selectChange} defaultValue={modelData ? modelData.map((item: any) => { return item.name }) : []}>
                        <Option key={1} value={1}>{1}</Option>
                        {modelData && modelData.map((item: any, index: number) => {
                            return (<Option key={index} value={item.name}>{item.name}</Option>)
                        })}
                    </Select>,
                </FormItem>)
            case ControlType.Switch:
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <Switch defaultChecked={modelData} />
                </FormItem>)
            case ControlType.CheckBox:
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <Checkbox checked></Checkbox>
                </FormItem>)
            case ControlType.CodeEditor:
                const aceStyle: CSS.Properties = {
                    width: '100%',
                    height: '800px'
                }
                const codeChange = (content: string) => {
                    var valueObj = {};
                    valueObj[modelTypeItem.propName] = content
                    form.setFieldsValue(valueObj)
                }
                return (<FormItem label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>

                    <AceEditor mode="json" theme="github" onChange={codeChange} value={modelData} style={aceStyle} >
                    </AceEditor>
                </FormItem>)
            case ControlType.TextArea_Editor:
                const editorChange = (contentState: any) => {
                    var valueObj = {};
                    valueObj[modelTypeItem.propName] = contentState.toHTML()
                    form.setFieldsValue(valueObj)
                }
                useEffect(() => {
                    setEditorState(BraftEditor.createEditorState(modelData))
                }, []);
                var filebaseService = new FilebaseService();
                return (
                    <FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]} >
                        <></>
                        <BraftEditorWrapper
                            value={editorState}
                            className={styles.bfContainer}
                            //defaultValue={editorState}
                            placeholder='请输入'
                            onChange={editorChange}
                            media={{
                                uploadFn: async (param) => {
                                    var imgUrl = await filebaseService.Upload(param.file.name, param.file)
                                    param.progress(100);
                                    param.success({
                                        url: imgUrl,
                                        meta: {
                                            id: imgUrl,
                                            title: imgUrl,
                                            alt: imgUrl,
                                            loop: false, // 指定音视频是否循环播放
                                            autoPlay: false, // 指定音视频是否自动播放
                                            controls: false, // 指定音视频是否显示控制栏
                                            poster: imgUrl, // 指定视频播放器的封面
                                        }
                                    })
                                }
                            }}></BraftEditorWrapper>

                    </FormItem>
                )
            case ControlType.Hidden:
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]} style={{ display: 'none' }}>

                </FormItem>)
            case ControlType.List:
                var children = Object.keys(modelTypeItem.dataSource).map(item => {
                    return (<Option value={item}>{modelTypeItem?.dataSource[item]}</Option>)
                });
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}  >

                    <Select style={{ width: 120 }} bordered={false}>
                        {children}
                    </Select>
                </FormItem>)
            case ControlType.Tree:
                const onSelect = (selectedKeys: any, info: any) => {
                };

                const onCheck = (checkedKeys: any, info: any) => {
                    var valueObj = {};

                    if (info.checkedNodes.length > 0) {
                        valueObj[modelTypeItem.propName] = info.checkedNodes[info.checkedNodes.length - 1].key
                        if (longIdKey) {
                            valueObj[longIdKey] = info.checkedNodes.map((item: any) => item.key).join(',');
                        }
                        form.setFieldsValue(valueObj)
                    } else {
                        valueObj[modelTypeItem.propName] = null
                        if (longIdKey) {
                            valueObj[longIdKey] = null
                        }
                        form.setFieldsValue(valueObj)
                    }
                };

                const onLoad = (data: any) => {
                    console.log(data)
                }

                const treeData = [];
                for (const key in modelTypeItem.dataSource) {
                    if (Object.prototype.hasOwnProperty.call(modelTypeItem.dataSource, key)) {
                        const value = modelTypeItem.dataSource[key];
                        treeData.push({
                            title: value,
                            key: key
                        })
                    }
                }
                return (
                    <FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: false }]}>
                        <Tree
                            checkable
                            defaultExpandedKeys={[modelData]}
                            defaultSelectedKeys={[modelData]}
                            defaultCheckedKeys={[modelData]}
                            onSelect={onSelect}
                            onCheck={onCheck}
                            treeData={treeData}
                            onLoad={onLoad}
                        />
                    </FormItem>
                )
            case ControlType.Label:
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <Input />
                </FormItem>)
            case ControlType.Img:

                var filebaseService = new FilebaseService();
                function getBase64(img: any, callback: (img: any) => void) {

                    const reader = new FileReader();
                    reader.addEventListener('load', () => callback(reader.result));
                    reader.readAsDataURL(img);
                }
                const handleChange = (info: any) => {
                    if (info.file.status === 'uploading') {
                        var loadobj = JSON.parse(JSON.stringify(loading))
                        loadobj[modelTypeItem.propName] = true
                        setLoading(loadobj)
                        return;
                    }
                    if (info.file.status === 'done') {
                        // Get this url from response in real world.
                        getBase64(info.file.originFileObj, (imageUrl: any) => {
                            var loadobj = JSON.parse(JSON.stringify(loading))
                            loadobj[modelTypeItem.propName] = false
                            setLoading(loadobj)

                            var imgobj = JSON.parse(JSON.stringify(imgUrl))
                            imgobj[modelTypeItem.propName] = imageUrl
                            setImgUrl(imgobj)
                        });
                        var valueObj = {};
                        valueObj[modelTypeItem.propName] = info.file.response
                        form.setFieldsValue(valueObj)
                    }
                };
                const uploadButton = (
                    <Button>
                        {loading[modelTypeItem.propName] ? <LoadingOutlined /> : <PlusOutlined />}
                        Upload
                    </Button>
                );

                return (
                    <FormItem label={modelTypeItem.name} key={modelTypeItem.propName} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}  >
                        <Upload
                            name={modelTypeItem.propName}
                            className={styles.imgUploader}
                            showUploadList={false}
                            customRequest={async (info: any) => {
                                var truePath = await filebaseService.Upload((new Date() - 0) + info.file.name, info.file)
                                info.onSuccess(truePath, info.file)
                            }}
                            onChange={handleChange}
                        >
                            {(imgUrl[modelTypeItem.propName] || modelData) ? <img src={(imgUrl[modelTypeItem.propName] || modelData)} alt="avatar" style={{ width: '100%' }} /> : uploadButton}
                        </Upload>
                    </FormItem>
                );

            case ControlType.Video:
                var filebaseService = new FilebaseService();
                const [videoType, setVideoType] = useState("iframe");
                const handelVideoChange = (info: any) => {
                    if (info.file.status === 'uploading') {
                        var loadobj = JSON.parse(JSON.stringify(loading))
                        loadobj[modelTypeItem.propName] = true
                        setLoading(loadobj)
                        return;
                    }
                    if (info.file.status === 'done') {
                        // Get this url from response in real world.
                        getBase64(info.file.originFileObj, (_videoUrl: any) => {
                            var loadobj = JSON.parse(JSON.stringify(loading))
                            loadobj[modelTypeItem.propName] = false
                            setLoading(loadobj)

                            var videoobj = JSON.parse(JSON.stringify(videoUrl))
                            videoobj[modelTypeItem.propName] = _videoUrl
                            setVideoUrl(videoobj)
                        });
                        var valueObj = {};
                        valueObj[modelTypeItem.propName] = info.file.response
                        form.setFieldsValue(valueObj)
                    }
                };
                const renderVideo = (poster: string, videoUrl: string) => {
                    return (
                        // <Reflv url={demo} type="flv"></Reflv>
                        <Player>
                            <source src={videoUrl} />
                        </Player>
                    )
                }
                return (
                    <>
                        <FormItem label="视频选项">
                            <Radio.Group onChange={(e: any) => { setVideoType(e.target.value) }} value={videoType}>
                                <Radio value={"origin"}>origin</Radio>
                                <Radio value={"iframe"}>iframe</Radio>
                            </Radio.Group>
                        </FormItem>
                        {
                            videoType == "origin" &&
                            <>
                                {  (videoUrl[modelTypeItem.propName] || modelData)
                                    ? renderVideo("", (videoUrl[modelTypeItem.propName] || modelData)) :
                                    ("")}
                                <FormItem label={modelTypeItem.name} key={modelTypeItem.propName} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}  >

                                    <Upload
                                        accept={".mp4"}
                                        name={modelTypeItem.propName}
                                        className={styles.imgUploader}
                                        showUploadList={false}
                                        customRequest={async (info: any) => {
                                            var truePath = await filebaseService.Upload((new Date() - 0) + info.file.name, info.file)
                                            info.onSuccess(truePath, info.file)
                                        }}
                                        onChange={handelVideoChange}
                                    >

                                        {(videoUrl[modelTypeItem.propName] || modelData)
                                            ? <Button>
                                                Replace
                                </Button> :
                                            <Button>
                                                Upload
                                 </Button>}
                                    </Upload>
                                </FormItem>
                            </>
                        }
                        {
                            videoType == "iframe" && (
                                <FormItem label={modelTypeItem.name} key={modelTypeItem.propName} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}  >
                                    <Input />
                                </FormItem>
                            )
                        }
                    </>
                );
            case ControlType.File:
                return (<Upload key={modelTypeItem.propName} {...props}>
                    <Button icon={<UploadOutlined />}>Click to Upload</Button>
                </Upload>)
            case ControlType.Date:
                var dateObj = {};
                dateObj[modelTypeItem.propName] = moment(modelData)
                form.setFieldsValue(dateObj)
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <DatePicker />
                </FormItem>)
            case ControlType.Number:
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <InputNumber />
                </FormItem>)
            case ControlType.Default:
            case ControlType.Input:
            default:
                return (<FormItem key={modelTypeItem.propName} label={modelTypeItem.name} name={modelTypeItem.propName} rules={[{ required: modelTypeItem.required }]}>
                    <Input />
                </FormItem>)
        }
    }
    const renderContent = () => {
        const formList = [];
        for (const modleItem of modelTypes) {
            let modelDataItem = modelData && modelData[modleItem.propName]
            formList.push(redenrModelItem(modleItem, modelDataItem))
        }

        return formList
    }
    if (useModal) {
        return (

            <Modal
                width={1024}
                visible={formModelVisible}
                maskClosable={false}
                onCancel={() => {
                    form.resetFields()
                    onCancel && onCancel()
                }}
                onOk={() => {
                    onSubmit(form.getFieldsValue())
                }}
            >
                <Form
                    {...formLayout}
                    form={form}
                    fields={fileds}
                    initialValues={data}
                >
                    {renderContent()}
                </Form>
            </Modal >)
    } else {
        return (
            <Form
                {...formLayout}
                form={form}
                fields={fileds}
                initialValues={data}
            >
                {renderContent()}
                <FormItem>
                    <Button type="primary" onClick={() => {
                        onSubmit(form.getFieldsValue())
                    }}>保存</Button>
                </FormItem>
            </Form>)
    }

}

DynamicForm.defaultProps = { useModal: true }
export default DynamicForm;