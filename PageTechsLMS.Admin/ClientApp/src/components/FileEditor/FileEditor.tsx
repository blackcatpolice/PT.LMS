import React, { useState, useRef, ReactNode } from 'react';
import CSS from 'csstype';
import { Layout, Tree, Button, Dropdown } from 'antd';
import { PageContainer, } from '@ant-design/pro-layout';
import { HotKeys } from "react-hotkeys";

import { Template, TemplateManagerClient } from '@/apis/API';
import AceEditor from "react-ace";
const { DirectoryTree } = Tree;

import { Menu, Item, Separator, Submenu, MenuProvider, contextMenu } from 'react-contexify';
import 'react-contexify/dist/ReactContexify.min.css';

import "ace-builds/src-noconflict/mode-html";
import "ace-builds/src-noconflict/theme-github";


import styles from './fileEditor.less'


const { Header, Footer, Sider, Content } = Layout;


const onClick = ({ event, props }: { event: any, props: any }) => {
    console.log(event)
    console.log(props)
};

const onContextMenu = (e: any, value: any) => {
    e.preventDefault();
    contextMenu.show({
        id: 'menu_id',
        event: e,
        props: {
            msg: value
        }
    });
}

// create your menu first
const MyAwesomeMenu = () => (
    <Menu id='menu_id' className={styles.ctxMenu}>
        <Item onClick={onClick}>Lorem</Item>
        <Item onClick={onClick}>Ipsum</Item>
        <Separator />
        <Item disabled>Dolor</Item>
        <Separator />
        <Submenu label="Foobar">
            <Item onClick={onClick}>Foo</Item>
            <Item onClick={onClick}>Bar</Item>
        </Submenu>
    </Menu>
);


const fileEditor: React.FC<{
    treeData: [],
    onOpenfile: (fileName: string) => Promise<string>;
    onSubmit: (filename: string, value: string) => void;
}> = (props: any) => {
    const { treeData, onOpenfile, onSubmit } = props
    const [content, setContent] = useState<string>();
    const [targetFileName, setTargetFileName] = useState<string>('');
    const [rightClickNodeTreeItem, setRightClickNodeTreeItem] = useState<any>();

    const contentTmp = []
    const contentCurrIndex = 0;
    const handleSave = () => {
        onSubmit(content);
    }
    const handleChange = (content: string) => {
        setContent(content)
    }

    const handleSelect = async (key: any, info: any) => {
        setTargetFileName(key)
        var content = await onOpenfile(key)
        setContent(content);
    }

    const handleExpand = () => {

    }

    const keyMap = {
        SaveFile: "command+s,ctrl+s",
    };
    const hotKeyHandler = {
        SaveFile: () => {
            onSubmit(targetFileName, content)
        }
    }
    const aceStyle: CSS.Properties = {
        width: '100%',
        height: '1000px'
    }
    // const treeNodeonRightClick = (event: any, node: any) => {
    //     event.persist();
    //     console.log(event)
    //     setRightClickNodeTreeItem({
    //         pageX: event.pageX - 220,
    //         pageY: event.pageY - 220,
    //         key: node.props.eventKey,
    //         id: node.props.eventKey,
    //         title: node.props.title,
    //     })
    // }

    return (
        <Layout  >
            <Header className={styles.header} >
                <Button onClick={() => {
                    onSubmit(targetFileName, content)
                }}>保存</Button>
                <Button>撤销</Button>
                <Button>重做</Button>
            </Header>
            <Layout>
                <Sider className={styles.sider} >

                    <Tree
                        className={styles.directoryTree}
                        defaultExpandAll
                        onSelect={handleSelect}
                        onExpand={handleExpand}
                        treeData={treeData}
                        titleRender={(node) => {
                            return (
                                <span onContextMenu={(event) => onContextMenu(event, node.key)}>{node.key} </span>
                            )
                        }}
                        onClick={() => {

                            setRightClickNodeTreeItem(null)
                        }}
                        onBlur={() => {

                            setRightClickNodeTreeItem(null)
                        }}
                    />

                    <MyAwesomeMenu />
                </Sider>
                <Content>
                    <HotKeys keyMap={keyMap} handlers={hotKeyHandler}>
                        <AceEditor mode="html" theme="github" onChange={handleChange} value={content} style={aceStyle} onFocus={() => {

                            setRightClickNodeTreeItem(null)

                        }}>

                        </AceEditor>
                    </HotKeys>
                </Content>
            </Layout>
            <Footer>Footer</Footer>
        </Layout >)
}

export default fileEditor;