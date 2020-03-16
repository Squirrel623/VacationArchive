import React, {FunctionComponent, useState} from 'react';
import axios, { AxiosResponse } from 'axios';
import {Form, Button} from 'react-bootstrap';

import {CreateResponse} from '../../../../generated-types/api/vacation/activity/media/create';
import { VacationActivityMedia } from '../../../../generated-types/api/vacation/activity/media/vacationActivityMedia';

export interface ActivityMediaUploaderProps {
  vacationId: number;
  activityId: number;
  onComplete?: (newMedia: VacationActivityMedia) => void;
}

export const ActivityMediaUploader: FunctionComponent<ActivityMediaUploaderProps> = (props) => {
  const [fileList, setFileList] = useState<FileList>()

  const handleFileChange = (event: React.FormEvent<HTMLInputElement>) => {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      setFileList(input.files);
    } else {
      setFileList(undefined);
    }
  }

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    if (!fileList || !fileList.length) {
      setFileList(undefined);
      return;
    }

    const url = `/api/vacations/${props.vacationId}/activities/${props.activityId}/media`;
    const file = fileList[0];
    const formData = new FormData();
    formData.append('file', file, file.name);

    const uploadData = async () => {
      try {
        const result = await axios.post<any, AxiosResponse<CreateResponse>>(url, formData);
        console.log(result.data);

        if (props.onComplete) {
          props.onComplete(result.data);
        }
        //debugger;
      } catch(error) {
        console.log(error);
        debugger;
      }
    };

    uploadData();
    event.preventDefault();
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group controlId="media-upload">
        <Form.Label>Select File to Upload</Form.Label>
        <Form.Control type="file" onChange={handleFileChange}></Form.Control>
      </Form.Group>
      <Button type="submit">Upload</Button>
    </Form>
  );
}