import React, { Component } from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Albums';
import axios from './AxiosInstance';
import Menu from './Menu';
import NotifierGenerator from './Notification';

class Albums extends Component {

    componentWillUnmount() {
        window.removeEventListener('scroll', this.onScroll, false);
        this.props.onBeforeRedirect();
    }

    constructor(props) {
        super(props);
        this.props.onInitAlbums()
        this.onScroll = this.onScroll.bind(this);
    }

    onScroll() {
        if (window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight) {
            if (this.props.hasMore) {
                (this.props.requestAlbums(this.props.pageIndex));
            }
        }
    }

    componentDidMount() {
        window.addEventListener('scroll', this.onScroll, false);
        this.props.requestAlbums(this.props.pageIndex);
    }

    render() {
        if (this.props.refresh) {
            this.props.requestAlbums(this.props.pageIndex)
        }

        if (this.props.redirectToRemove) {
            const selectedAlbums = this.props.albums.filter(album => album.isSelected === true);
            const albums = selectedAlbums.map(album => ({ albumId: album.albumId }))
            const onRemoveSuccess = this.props.onRemoveSuccess
            const onRemoveError = this.props.onRemoveError
            var request = axios.delete(`api/Albums/RemoveAlbum`, albums[0])
            request
                .then(function (response) {
                    if (response.status === 200) {
                        onRemoveSuccess()
                    }
                })
                .catch(function (error) {
                    if (error.response && error.response.data) {
                        onRemoveError(error.response.data)
                    }
                });
        }

        if (this.props.redirectToAlbum) {
            return (<Redirect push to={{ pathname: `/album/${this.props.albumId}`, state: { selectedAlbum: this.props.albums.filter(album => album.isSelected)[0] || this.props.albums.filter(album => album.albumId === this.props.albumId)[0] } }} />)
        }
        if (this.props.redirectToEdit || this.props.redirectToAdd) {
            return (<Redirect push to={{ pathname: `/albumEdit/${this.props.albumId}`, state: { selectedAlbum: this.props.albums.filter(album => album.isSelected)[0] || { photos: [] } } }} />)
        }
        return (
            <div>
                <NotifierGenerator messageText={this.props.alertMessage}
                    messageId={this.props.alertId}
                    messageType={this.props.alertType} />

                <h1 className="title">Albums Gallery</h1>
                {renderMenu(this.props)}
                {renderAlbumsGallery(this.props)}
            </div >
        );
    }
}

function renderMenu(props) {
    if (localStorage.getItem("token")) {
        return (<Menu onEditCommand={props.onAlbumEditClick}
            onViewCommand={props.onAlbumViewClick}
            onAddCommand={props.onAlbumAddClick}
            onRemoveCommand={props.onAlbumRemoveClick}
        />)
    }
}

function renderAlbumsGallery(props) {
    const albums = props.albums || [];
    return (
        <div>
            <Gallery images={albums.map(album => ({
                src: `${album.coverUrl}?v=${props.currentDate}`,
                thumbnail: `${album.coverUrl}?v=${props.currentDate}`,
                isSelected: album.isSelected,
                caption: album.description,
                thumbnailWidth: 300,
                thumbnailHeight: 170,
                tags: [{ value: album.albumName, title: album.albumName }]
            }))}
                margin={30}
                rowHeight={250}
                onSelectImage={props.onSelectImage}
                onClickThumbnail={props.onAlbumThumbnailClick} />
        </div>
    );
}

export default connect(
    state => state.albums,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Albums);