import React ,{useEffect}from 'react';
import BraftEditor, { ControlType } from 'braft-editor';
import 'braft-editor/dist/index.css'
import { BraftEditorProps } from 'braft-editor'

const BraftEditorWrapper: React.FC<BraftEditorProps> = (props) => {
      
    return (<BraftEditor
        {...props}
    ></BraftEditor>)
}

export default BraftEditorWrapper;

