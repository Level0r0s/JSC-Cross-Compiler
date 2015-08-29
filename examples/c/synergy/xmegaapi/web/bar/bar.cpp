
// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150827
// can we have jsc c native link to a c file via assets?
// first lets do it manually.
// idl for c?

#ifndef __cplusplus
#error "is only for C++!"
#endif

// bar/bar.cpp(18) : error C2653: 'bar' : is not a class or namespace name
#include "bar.h"

extern "C" {
	// called by  Z:\jsc.svn\examples\c\synergy\xmegaapi\web\foo\foo.c

	char* abc_bar_barinvoke()
	{
		//return new abc::bar()->bar_barinvoke();
		return (new abc::bar())->bar_barinvoke();
		//return "program.cs -> foo.c -> bar.h -> bar.cpp extern C -> bar::bar_barinvoke";
	}
}


#include <megaapi.h>
#include <Windows.h>
#include <iostream>

using namespace mega;
using namespace std;

namespace abc
{
	// can we compile this can have it called from c?

	//char* bar::bar_barinvoke(void)

	// bar/bar.cpp(18) : error C2653: 'bar' : is not a class or namespace name



	class MyListener : public MegaListener
	{
	public:
		bool finished;

		MyListener()
		{
			finished = false;
		}

		virtual void onRequestFinish(MegaApi* api, MegaRequest *request, MegaError* e)
		{
			if (e->getErrorCode() != MegaError::API_OK)
			{
				finished = true;
				return;
			}

			switch (request->getType())
			{
			case MegaRequest::TYPE_LOGIN:
			{
				api->fetchNodes();
				break;
			}
			case MegaRequest::TYPE_FETCH_NODES:
			{
				cout << "***** Showing files/folders in the root folder:" << endl;
				MegaNode *root = api->getRootNode();
				MegaNodeList *list = api->getChildren(root);

				for (int i = 0; i < list->size(); i++)
				{
					MegaNode *node = list->get(i);
					if (node->isFile())
						cout << "*****   File:   ";
					else
						cout << "*****   Folder: ";

					cout << node->getName() << endl;
				}
				cout << "***** Done" << endl;

				delete list;

				cout << "***** Uploading the ..." << endl;
				api->startUpload("xmegaapi.exe", root);
				delete root;

				break;
			}
			default:
				break;
			}
		}

		//Currently, this callback is only valid for the request fetchNodes()
		virtual void onRequestUpdate(MegaApi*api, MegaRequest *request)
		{
			cout << "***** Loading filesystem " << request->getTransferredBytes() << " / " << request->getTotalBytes() << endl;
		}

		virtual void onRequestTemporaryError(MegaApi *api, MegaRequest *request, MegaError* error)
		{
			cout << "***** Temporary error in request: " << error->getErrorString() << endl;
		}

		virtual void onTransferFinish(MegaApi* api, MegaTransfer *transfer, MegaError* error)
		{
			if (error->getErrorCode())
			{
				cout << "***** Transfer finished with error: " << error->getErrorString() << endl;
			}
			else
			{
				cout << "***** Transfer finished OK" << endl;
			}

			finished = true;
		}

		virtual void onTransferUpdate(MegaApi *api, MegaTransfer *transfer)
		{
			cout << "***** Transfer progress: " << transfer->getTransferredBytes() << "/" << transfer->getTotalBytes() << endl;
		}

		virtual void onTransferTemporaryError(MegaApi *api, MegaTransfer *transfer, MegaError* error)
		{
			cout << "***** Temporary error in transfer: " << error->getErrorString() << endl;
		}

		virtual void onUsersUpdate(MegaApi* api, MegaUserList *users)
		{
			cout << "***** There are " << users->size() << " new or updated users in your account" << endl;
		}

		virtual void onNodesUpdate(MegaApi* api, MegaNodeList *nodes)
		{
			if (nodes == NULL)
			{
				//Full account reload
				return;
			}

			cout << "***** There are " << nodes->size() << " new or updated node/s in your account" << endl;
		}
	};


	//ENTER YOUR CREDENTIALS HERE
#define MEGA_EMAIL "1qk2bf+7sz2dc14r59v4@guerrillamailblock.com"
#define MEGA_PASSWORD "oiumrtin"

	//Get yours for free at https://mega.co.nz/#sdk
#define APP_KEY "X1cWFRIB"
#define USER_AGENT "Example Win32 App"

	char* bar::bar_barinvoke(void)
	{
		cout << "enter bar_barinvoke" << endl;

		auto megaApi = new MegaApi(APP_KEY, (const char *)NULL, USER_AGENT);


		//By default, logs are sent to stdout
		//You can use MegaApi::setLoggerObject to receive SDK logs in your app
		megaApi->setLogLevel(MegaApi::LOG_LEVEL_MAX);

		MegaProxy p;
		cout << "before setProxyType" << endl;

		// https://www.chromium.org/developers/design-documents/network-stack/socks-proxy
		p.setProxyType(MegaProxy::PROXY_CUSTOM);
		p.setProxyURL("socks5://127.0.0.1:9150");
		megaApi->setProxySettings(&p);
		MyListener listener;

		// void MegaApi::setProxySettings(MegaProxy *proxySettings)


		//Listener to receive information about all request and transfers
		//It is also possible to register a different listener per request/transfer
		megaApi->addListener(&listener);

		if (!strcmp(MEGA_EMAIL, "EMAIL"))
		{
			cout << "Please enter your email/password at the top of main.cpp" << endl;
			cout << "Press any key to exit the app..." << endl;
			getchar();
			exit(0);
		}

		//Login. You can get the result in the onRequestFinish callback of your listener
		megaApi->login(MEGA_EMAIL, MEGA_PASSWORD);

		//You can use the main thread to show a GUI or anything else. MegaApi runs in a background thread.
		while (!listener.finished)
		{
			Sleep(1000);
		}

		cout << "Press any key to exit the app..." << endl;
		getchar();


		// how can we call this from our code?
		return "program.cs -> foo.c -> bar.h -> bar.cpp extern C -> bar::bar_barinvoke";
		//return "hello from bar.cpp";
		// can jsc do cpp yet?
	}

}

