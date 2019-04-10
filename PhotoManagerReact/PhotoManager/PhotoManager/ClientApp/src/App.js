import React from 'react';
import 'react-notifications/lib/notifications.css';
import { Route } from 'react-router';
import AlbumEdit from './components/AlbumEdit';
import AlbumPhotos from './components/AlbumPhotos';
import Albums from './components/Albums';
import Home from './components/Home';
import Layout from './components/Layout';
import Login from './components/Login';
import PhotoAdd from './components/PhotoAdd';
import PhotoEdit from './components/PhotoEdit';
import PhotoGallery from './components/PhotoGallery';
import './components/PhotoManager.css';
import PhotoView from './components/PhotoView';
import Register from './components/Register';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/Login' component={Login} />
        <Route exact path='/Register' component={Register} />
        <Route path='/album/:albumId' component={AlbumPhotos} />
        <Route path='/albums/:pageIndex?' component={Albums} />
        <Route path='/albumEdit/:albumId' component={AlbumEdit} />
        <Route path='/photoEdit/' component={PhotoEdit} />
        <Route path='/photoView/' component={PhotoView} />
        <Route path='/photoAdd/:albumId' component={PhotoAdd} />
        <Route path='/photoGallery/:pageIndex' component={PhotoGallery} />
    </Layout>
);
