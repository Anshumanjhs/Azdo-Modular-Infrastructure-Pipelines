# Azure-PrivateDevopsAgent-Demos-Ansible-Terraform-Pipelines

### Main Build Status 

[![Build Status](https://dev.azure.com/csebraveheart/Azdo-Modular-Infrastructure-Pipelines/_apis/build/status/Pre-provisioning?branchName=main)](https://dev.azure.com/csebraveheart/Azdo-Modular-Infrastructure-Pipelines/_build/latest?definitionId=36&branchName=main)

### Staging Build Status 
[![Build Status](https://dev.azure.com/csebraveheart/Azdo-Modular-Infrastructure-Pipelines/_apis/build/status/Pre-provisioning?branchName=staging)](https://dev.azure.com/csebraveheart/Azdo-Modular-Infrastructure-Pipelines/_build/latest?definitionId=36&branchName=staging)
- ^ *This would be a customer's first edit* 

  
## Summary 

This solution was designed to support infrastructure 'module' development for Azure. It supports developers with a platform for flexible deployment methodologies. Provided in this repository is a sample of techniques and core infrastructure required for an API that triggers Azure Devops Build Pipelines for managing Azure Resources. The solution was designed to expose pipelines in a secure and extensible way providing customization for any client application and backend requirements.  

One of the key features of this solution is the ability to manage resources in a private network. The solution can be configured to isolate execution to a private azdo agent, which becomes the exclusive entry-point to a customer architecture. 

The solution is still under development, but is ready for deployment and module creation in development subscriptions.  

To deploy the solution your team will need an Azure Devops organization an Azure Key Vault with service principal credentials provisioned and to create and trigger the pre-provisioning azure pipeline. (See demo video)

To add modules to your solution, create a pipeline in Azure Devops and point to the already existing yaml in each module. (prefixed with 'azdo-' as a convention)

We also recommended watching our video for deployment instructions here: [demo video](https://msit.microsoftstream.com/video/2d40a1ff-0400-9fb2-790d-f1eb04dc9df9)

And if you have any questions feel free to reach out directly to the team. 

### Contents of this repository include a number of patterns for building with Azure Devops:  

- Deploying idempotent network configuration using terraform and ansible: [network module](src/modules/network/)

- Creating a vm to host an Azure Devops Private Agent with Ansible: [ubuntu azdo agent](src/modules/ubuntu_azdo_agent)

- Creating a an ubuntu vm with terraform and configuring post deployment with ansible: [ubuntu worker](src/modules/ubuntu_worker)

- Creating a new windows vm with terraform, configuring winrm, and configuring post deployment with ansible: [windows machine ready for config](src/modules/windows_worker)

- Measuring network throughput on the azure backbone with go: [network tests](src/network-tests)

## Running and Developing Modules

Dependencies: 
- A linux terminal with Python 3.6+ and pip or docker or see run with docker below. 
    - To get up and running with WSL see Reference Material > [Getting started with WSL for Python3 and Pip3](#getting-started-with-wsl-for-python3-and-pip3) 

1. Create and activate virtual env 
    - `python3 -m venv venv` 
    - `source venv/bin/activate` 
1. Install project dependencies with pip
`pip install -r requirements.txt`
1. Install ansible: `pip3 install ansible` 
1. Validate ansible installation
`ansible --version` 
1. Run `sudo ./ubuntu_host_setup.sh` 
1. export SECRETS/Subscription information, app-insights keys, and debugging level/format. 

```
Sample ENV File
export ANSIBLE_STDOUT_CALLBACK=debug
export SUBSCRIPTION_NAME=whgriffi 
export STORAGE_ACCOUNT_NAME=corestorageaccountwus 
export CONTAINER_NAME=state 
export ARM_TENANT_ID=SECRET86f1-41af-91ab-2d7cd011db47
export ARM_CLIENT_SECRET=SECRETKwfFd~FRKkmvb8h7Qu8A_8xbW
export ARM_CLIENT_ID=SECRET56ef-4c21-bae6-a6c32c7cb9c5
export ARM_SUBSCRIPTION_ID=SECRET1982-4b4d-9efa-a9b845a55b13
export ARM_ACCESS_KEY=SECRETxfghAvvsFF9OqWeVNs5zEpWYpvRF15StV1j7Mch93kjRw6F+k12v0RZrL7xlufKl9H5KRagcmk9SA== 
```
Then you can run any playbook in the solution...
See example usage in each module:
- [network module example usage](/src/modules/network/example_usage.md) 
- [ubuntu worker example usage](/src/modules/ubuntu_worker/example_usage.md)

After code is validated locally or in a jumpbox, you can commit and deploy to azure devops using Azure Pipelines, this requires opening the organization and adding a pipeline and pointing to the existing azdo pipeline yaml in the module.  (prefixed 'azdo-' by convention)

## To Run with Docker

### Build and run docker container in the background
`docker-compose -f .\setup\docker-compose.yml up -d`

### List of existing images
`docker images`

### List of currently running containers
`docker ps`

### Attach to existing container (Container id can be copied from previous command)
`docker exec -it CONTAINER_ID bash`

### Or run a playbook directly (still need to determine how we're going to setup local credentials): 
`docker exec CONTAINER_ID ansible-playbook Solution/tests/pre_runner.yml`

Now you're attached to the docker container. Our solution can be found in the solution directory. 

Run pre-runner to verify environment/check for required environment variables. 

`ansible:/home/ansible/Solution$ ansible-playbook tests/pre_runner.yml`

### All together: 
```
$ docker-compose -f .\setup\docker-compose.yml up -d
setup_ansible_1 is up-to-date

$ docker ps
CONTAINER ID        IMAGE               COMMAND               CREATED             STATUS              PORTS               NAMES
22bcba10f73e        setup_ansible       "tail -f /dev/null"   6 minutes ago       Up 6 minutes                            setup_ansible_1

$ docker exec -it 22bcba10f73e bash

ansible:/home/ansible$ cd Solution

ansible:/home/ansible/Solution$ ansible-playbook tests/pre_runner.yml 
[WARNING]: No inventory was parsed, only implicit localhost is available
[WARNING]: provided hosts list is empty, only localhost is available. Note that the implicit localhost does not match 'all'

PLAY [build worker] *****************************************************************************************************************************************************************************************************************************
TASK [Gathering Facts] ****************************************************************************************************************************************************************************************************************************************ok: [localhost]
'etc...'
```
## To tear down docker locally 
1. Delete images if you'd like.  
2. Quit docker from the taskbar. 

# Next Steps: 
- Add instructions for private installation media 
- Create modules for peering and private endpoints 

# Reference Material: 

## Getting started with WSL for Python3 and Pip3 

Install ubuntu 18.04 distribution on your windows machine using these instructions: 
Connect to that machine
```
> wsl --set-default Ubuntu-18.04
> bash 
```

Run these commands in your new linux shell 

```
$ sudo apt-get update && sudo apt-get upgrade
$ sudo apt install software-properties-common
$ sudo apt-get install gcc libpq-dev build-essential libssl-dev libffi-dev -y
$ sudo apt-get install python3-pip python3-venv -y
```

Create a virtual environment 

```
$ python3 -m venv venv 
$ source venv/bin/activate 
$ pip3 install wheel
$ pip3 list 
```

Now you can install ansible or a `requirements.txt` or ansible directly 

```
$ pip3 install ansible 
$ pip3 install -r requirements.txt
```

# Troubleshooting 
