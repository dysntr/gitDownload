namespace gitDownload.fs

module Types =
    open FSharp.Data

    type Repo =
        { user : string 
          repo : string 
          ref : string 
          dir : string 
          
        }
        with
            static member Default = {user = ""; repo = ""; ref = ""; dir = ""}

    type DirContent =
        {
            Path:string
            Type:string
            Url:string
            Name:string
        }
        with
            static member Default = {Path = ""; Type = ""; Url = ""; Name = "" }
    
    type FileSample = JsonProvider<"""{
            "sha": "b0eda9a5fd4137dd6bcdc5bb9c8c49239f84f100",
            "node_id": "MDQ6QmxvYjM1NzM3MDQzNzpiMGVkYTlhNWZkNDEzN2RkNmJjZGM1YmI5YzhjNDkyMzlmODRmMTAw",
            "size": 3961,
            "url": "https://api.github.com/repos/tinchoabbate/damn-vulnerable-defi/git/blobs/b0eda9a5fd4137dd6bcdc5bb9c8c49239f84f100",
            "content": "Ly8gU1BEWC1MaWNlbnNlLUlkZW50aWZpZXI6IE1JVApwcmFnbWEgc29saWRp\ndHkgXjAuOC4wOwoKaW1wb3J0ICJAb3BlbnplcHBlbGluL2NvbnRyYWN0cy9h\nY2Nlc3MvQWNjZXNzQ29udHJvbC5zb2wiOwppbXBvcnQgIkBvcGVuemVwcGVs\naW4vY29udHJhY3RzL3V0aWxzL0FkZHJlc3Muc29sIjsKCi8qKgogKiBAdGl0\nbGUgQ2xpbWJlclRpbWVsb2NrCiAqIEBhdXRob3IgRGFtbiBWdWxuZXJhYmxl\nIERlRmkgKGh0dHBzOi8vZGFtbnZ1bG5lcmFibGVkZWZpLnh5eikKICovCmNv\nbnRyYWN0IENsaW1iZXJUaW1lbG9jayBpcyBBY2Nlc3NDb250cm9sIHsKICAg\nIHVzaW5nIEFkZHJlc3MgZm9yIGFkZHJlc3M7CgogICAgYnl0ZXMzMiBwdWJs\naWMgY29uc3RhbnQgQURNSU5fUk9MRSA9IGtlY2NhazI1NigiQURNSU5fUk9M\nRSIpOwogICAgYnl0ZXMzMiBwdWJsaWMgY29uc3RhbnQgUFJPUE9TRVJfUk9M\nRSA9IGtlY2NhazI1NigiUFJPUE9TRVJfUk9MRSIpOwoKICAgIC8vIFBvc3Np\nYmxlIHN0YXRlcyBmb3IgYW4gb3BlcmF0aW9uIGluIHRoaXMgdGltZWxvY2sg\nY29udHJhY3QKICAgIGVudW0gT3BlcmF0aW9uU3RhdGUgewogICAgICAgIFVu\na25vd24sCiAgICAgICAgU2NoZWR1bGVkLAogICAgICAgIFJlYWR5Rm9yRXhl\nY3V0aW9uLAogICAgICAgIEV4ZWN1dGVkCiAgICB9CgogICAgLy8gT3BlcmF0\naW9uIGRhdGEgdHJhY2tlZCBpbiB0aGlzIGNvbnRyYWN0CiAgICBzdHJ1Y3Qg\nT3BlcmF0aW9uIHsKICAgICAgICB1aW50NjQgcmVhZHlBdFRpbWVzdGFtcDsg\nICAvLyB0aW1lc3RhbXAgYXQgd2hpY2ggdGhlIG9wZXJhdGlvbiB3aWxsIGJl\nIHJlYWR5IGZvciBleGVjdXRpb24KICAgICAgICBib29sIGtub3duOyAgICAg\nICAgIC8vIHdoZXRoZXIgdGhlIG9wZXJhdGlvbiBpcyByZWdpc3RlcmVkIGlu\nIHRoZSB0aW1lbG9jawogICAgICAgIGJvb2wgZXhlY3V0ZWQ7ICAgICAgLy8g\nd2hldGhlciB0aGUgb3BlcmF0aW9uIGhhcyBiZWVuIGV4ZWN1dGVkCiAgICB9\nCgogICAgLy8gT3BlcmF0aW9ucyBhcmUgdHJhY2tlZCBieSB0aGVpciBieXRl\nczMyIGlkZW50aWZpZXIKICAgIG1hcHBpbmcoYnl0ZXMzMiA9PiBPcGVyYXRp\nb24pIHB1YmxpYyBvcGVyYXRpb25zOwoKICAgIHVpbnQ2NCBwdWJsaWMgZGVs\nYXkgPSAxIGhvdXJzOwoKICAgIGNvbnN0cnVjdG9yKAogICAgICAgIGFkZHJl\nc3MgYWRtaW4sCiAgICAgICAgYWRkcmVzcyBwcm9wb3NlcgogICAgKSB7CiAg\nICAgICAgX3NldFJvbGVBZG1pbihBRE1JTl9ST0xFLCBBRE1JTl9ST0xFKTsK\nICAgICAgICBfc2V0Um9sZUFkbWluKFBST1BPU0VSX1JPTEUsIEFETUlOX1JP\nTEUpOwoKICAgICAgICAvLyBkZXBsb3llciArIHNlbGYgYWRtaW5pc3RyYXRp\nb24KICAgICAgICBfc2V0dXBSb2xlKEFETUlOX1JPTEUsIGFkbWluKTsKICAg\nICAgICBfc2V0dXBSb2xlKEFETUlOX1JPTEUsIGFkZHJlc3ModGhpcykpOwoK\nICAgICAgICBfc2V0dXBSb2xlKFBST1BPU0VSX1JPTEUsIHByb3Bvc2VyKTsK\nICAgIH0KCiAgICBmdW5jdGlvbiBnZXRPcGVyYXRpb25TdGF0ZShieXRlczMy\nIGlkKSBwdWJsaWMgdmlldyByZXR1cm5zIChPcGVyYXRpb25TdGF0ZSkgewog\nICAgICAgIE9wZXJhdGlvbiBtZW1vcnkgb3AgPSBvcGVyYXRpb25zW2lkXTsK\nICAgICAgICAKICAgICAgICBpZihvcC5leGVjdXRlZCkgewogICAgICAgICAg\nICByZXR1cm4gT3BlcmF0aW9uU3RhdGUuRXhlY3V0ZWQ7CiAgICAgICAgfSBl\nbHNlIGlmKG9wLnJlYWR5QXRUaW1lc3RhbXAgPj0gYmxvY2sudGltZXN0YW1w\nKSB7CiAgICAgICAgICAgIHJldHVybiBPcGVyYXRpb25TdGF0ZS5SZWFkeUZv\nckV4ZWN1dGlvbjsKICAgICAgICB9IGVsc2UgaWYob3AucmVhZHlBdFRpbWVz\ndGFtcCA+IDApIHsKICAgICAgICAgICAgcmV0dXJuIE9wZXJhdGlvblN0YXRl\nLlNjaGVkdWxlZDsKICAgICAgICB9IGVsc2UgewogICAgICAgICAgICByZXR1\ncm4gT3BlcmF0aW9uU3RhdGUuVW5rbm93bjsKICAgICAgICB9CiAgICB9Cgog\nICAgZnVuY3Rpb24gZ2V0T3BlcmF0aW9uSWQoCiAgICAgICAgYWRkcmVzc1td\nIGNhbGxkYXRhIHRhcmdldHMsCiAgICAgICAgdWludDI1NltdIGNhbGxkYXRh\nIHZhbHVlcywKICAgICAgICBieXRlc1tdIGNhbGxkYXRhIGRhdGFFbGVtZW50\ncywKICAgICAgICBieXRlczMyIHNhbHQKICAgICkgcHVibGljIHB1cmUgcmV0\ndXJucyAoYnl0ZXMzMikgewogICAgICAgIHJldHVybiBrZWNjYWsyNTYoYWJp\nLmVuY29kZSh0YXJnZXRzLCB2YWx1ZXMsIGRhdGFFbGVtZW50cywgc2FsdCkp\nOwogICAgfQoKICAgIGZ1bmN0aW9uIHNjaGVkdWxlKAogICAgICAgIGFkZHJl\nc3NbXSBjYWxsZGF0YSB0YXJnZXRzLAogICAgICAgIHVpbnQyNTZbXSBjYWxs\nZGF0YSB2YWx1ZXMsCiAgICAgICAgYnl0ZXNbXSBjYWxsZGF0YSBkYXRhRWxl\nbWVudHMsCiAgICAgICAgYnl0ZXMzMiBzYWx0CiAgICApIGV4dGVybmFsIG9u\nbHlSb2xlKFBST1BPU0VSX1JPTEUpIHsKICAgICAgICByZXF1aXJlKHRhcmdl\ndHMubGVuZ3RoID4gMCAmJiB0YXJnZXRzLmxlbmd0aCA8IDI1Nik7CiAgICAg\nICAgcmVxdWlyZSh0YXJnZXRzLmxlbmd0aCA9PSB2YWx1ZXMubGVuZ3RoKTsK\nICAgICAgICByZXF1aXJlKHRhcmdldHMubGVuZ3RoID09IGRhdGFFbGVtZW50\ncy5sZW5ndGgpOwoKICAgICAgICBieXRlczMyIGlkID0gZ2V0T3BlcmF0aW9u\nSWQodGFyZ2V0cywgdmFsdWVzLCBkYXRhRWxlbWVudHMsIHNhbHQpOwogICAg\nICAgIHJlcXVpcmUoZ2V0T3BlcmF0aW9uU3RhdGUoaWQpID09IE9wZXJhdGlv\nblN0YXRlLlVua25vd24sICJPcGVyYXRpb24gYWxyZWFkeSBrbm93biIpOwog\nICAgICAgIAogICAgICAgIG9wZXJhdGlvbnNbaWRdLnJlYWR5QXRUaW1lc3Rh\nbXAgPSB1aW50NjQoYmxvY2sudGltZXN0YW1wKSArIGRlbGF5OwogICAgICAg\nIG9wZXJhdGlvbnNbaWRdLmtub3duID0gdHJ1ZTsKICAgIH0KCiAgICAvKiog\nQW55b25lIGNhbiBleGVjdXRlIHdoYXQgaGFzIGJlZW4gc2NoZWR1bGVkIHZp\nYSBgc2NoZWR1bGVgICovCiAgICBmdW5jdGlvbiBleGVjdXRlKAogICAgICAg\nIGFkZHJlc3NbXSBjYWxsZGF0YSB0YXJnZXRzLAogICAgICAgIHVpbnQyNTZb\nXSBjYWxsZGF0YSB2YWx1ZXMsCiAgICAgICAgYnl0ZXNbXSBjYWxsZGF0YSBk\nYXRhRWxlbWVudHMsCiAgICAgICAgYnl0ZXMzMiBzYWx0CiAgICApIGV4dGVy\nbmFsIHBheWFibGUgewogICAgICAgIHJlcXVpcmUodGFyZ2V0cy5sZW5ndGgg\nPiAwLCAiTXVzdCBwcm92aWRlIGF0IGxlYXN0IG9uZSB0YXJnZXQiKTsKICAg\nICAgICByZXF1aXJlKHRhcmdldHMubGVuZ3RoID09IHZhbHVlcy5sZW5ndGgp\nOwogICAgICAgIHJlcXVpcmUodGFyZ2V0cy5sZW5ndGggPT0gZGF0YUVsZW1l\nbnRzLmxlbmd0aCk7CgogICAgICAgIGJ5dGVzMzIgaWQgPSBnZXRPcGVyYXRp\nb25JZCh0YXJnZXRzLCB2YWx1ZXMsIGRhdGFFbGVtZW50cywgc2FsdCk7Cgog\nICAgICAgIGZvciAodWludDggaSA9IDA7IGkgPCB0YXJnZXRzLmxlbmd0aDsg\naSsrKSB7CiAgICAgICAgICAgIHRhcmdldHNbaV0uZnVuY3Rpb25DYWxsV2l0\naFZhbHVlKGRhdGFFbGVtZW50c1tpXSwgdmFsdWVzW2ldKTsKICAgICAgICB9\nCiAgICAgICAgCiAgICAgICAgcmVxdWlyZShnZXRPcGVyYXRpb25TdGF0ZShp\nZCkgPT0gT3BlcmF0aW9uU3RhdGUuUmVhZHlGb3JFeGVjdXRpb24pOwogICAg\nICAgIG9wZXJhdGlvbnNbaWRdLmV4ZWN1dGVkID0gdHJ1ZTsKICAgIH0KCiAg\nICBmdW5jdGlvbiB1cGRhdGVEZWxheSh1aW50NjQgbmV3RGVsYXkpIGV4dGVy\nbmFsIHsKICAgICAgICByZXF1aXJlKG1zZy5zZW5kZXIgPT0gYWRkcmVzcyh0\naGlzKSwgIkNhbGxlciBtdXN0IGJlIHRpbWVsb2NrIGl0c2VsZiIpOwogICAg\nICAgIHJlcXVpcmUobmV3RGVsYXkgPD0gMTQgZGF5cywgIkRlbGF5IG11c3Qg\nYmUgMTQgZGF5cyBvciBsZXNzIik7CiAgICAgICAgZGVsYXkgPSBuZXdEZWxh\neTsKICAgIH0KCiAgICByZWNlaXZlKCkgZXh0ZXJuYWwgcGF5YWJsZSB7fQp9\nCg==\n",
            "encoding": "base64"
            }""">
    
    
    type ContentList = JsonProvider<"""{
                        "sha": "3e2a3675f4a733557ee9417b97fee104c0110618",
                        "url": "https://api.github.com/repos/tinchoabbate/damn-vulnerable-defi/git/trees/3e2a3675f4a733557ee9417b97fee104c0110618",
                        "tree": [{
                            "path": ".gitignore",
                            "mode": "100644",
                            "type": "blob",
                            "sha": "fbdfcada951d828404c16ef00471eac38a82fcbf",
                            "size": 110,
                            "url": "https://api.github.com/repos/tinchoabbate/damn-vulnerable-defi/git/blobs/fbdfcada951d828404c16ef00471eac38a82fcbf"
                        }, {
                            "path": "CHANGELOG.md",
                            "mode": "100644",
                            "type": "blob",
                            "sha": "98bb58363faa57f512e127038c020799703911ee",
                            "size": 1003,
                            "url": "https://api.github.com/repos/tinchoabbate/damn-vulnerable-defi/git/blobs/98bb58363faa57f512e127038c020799703911ee"
                        }],
                        "truncated": false
                    }""">