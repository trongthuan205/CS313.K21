#du lieu moi duoc  luu trong  file data
def PrepareData(file):
    with open(file, 'r', encoding='utf-8') as file_doc:
        with open('DATA.txt', 'w', encoding='utf-8') as file_ghi:
            for line in file_doc:
                i = 0
                chuoi= ''
                while(i<len(line)):
                    if(line[i]=='"'):
                        i = i +1
                        while(i<len(line)):
                            if(line[i]=='"'):
                                i = i+1
                                chuoi = chuoi+ '""'
                                break
                            else:
                                i = i+1
                    else:
                        chuoi = chuoi+line[i]
                        i=i+1
                vitri_dp = []
                i= 0
                while(i<len(chuoi)):
                    if(chuoi[i]==','):
                        vitri_dp.append(i)
                    i=i+1
                thuoctinh = 0 # so luong thuoc tinh 18
                while(thuoctinh<len(vitri_dp)):
                    if(thuoctinh==0):
                        i = 0
                        while(i<vitri_dp[0]):
                            file_ghi.write(chuoi[i])
                            i=i+1
                        file_ghi.write(',')
                    elif(thuoctinh==1):
                        if((vitri_dp[1]-vitri_dp[0])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[0]+1
                            while(i<vitri_dp[1]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==2):
                        if((vitri_dp[1]-vitri_dp[0])==1):
                            file_ghi.write('?,')
                        else:
                            file_ghi.write('1,')
                    elif(thuoctinh==3):
                        if((vitri_dp[1]-vitri_dp[0])==1):
                            file_ghi.write('?,')
                        else:
                            file_ghi.write('1,')
                    elif(thuoctinh==4):
                        if((vitri_dp[4]-vitri_dp[3])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[3]+1
                            while(i<vitri_dp[4]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==5):
                        if((vitri_dp[5]-vitri_dp[4])==1):
                            file_ghi.write('?,')
                        else:
                            file_ghi.write('1,')
                    elif(thuoctinh==6):
                        if((vitri_dp[6]-vitri_dp[5])==1):
                            file_ghi.write('?,')
                        else:
                            file_ghi.write('1,')
                    elif(thuoctinh==7):
                        if((vitri_dp[7]-vitri_dp[6])==1):
                            file_ghi.write('?,')
                        else:
                            file_ghi.write('1,')
                    elif(thuoctinh==8):
                        if((vitri_dp[8]-vitri_dp[7])==1):
                            file_ghi.write('?,')
                        else:
                            file_ghi.write('1,')
                    elif(thuoctinh==9):
                        if((vitri_dp[9]-vitri_dp[8])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[8]+1
                            while(i<vitri_dp[9]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==10):
                        if((vitri_dp[10]-vitri_dp[9])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[9]+1
                            while(i<vitri_dp[10]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==11):
                        if((vitri_dp[11]-vitri_dp[10])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[10]+1
                            while(i<vitri_dp[11]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==12):
                        if((vitri_dp[12]-vitri_dp[11])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[11]+1
                            while(i<vitri_dp[12]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==13):
                        if((vitri_dp[13]-vitri_dp[12])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[12]+1
                            while(i<vitri_dp[13]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==14):
                        if((vitri_dp[14]-vitri_dp[13])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[13]+1
                            while(i<vitri_dp[14]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==15):
                        if((vitri_dp[15]-vitri_dp[14])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[14]+1
                            while(i<vitri_dp[15]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==16):
                        if((vitri_dp[16]-vitri_dp[15])==1):
                            file_ghi.write('?,')
                        else:
                            i=vitri_dp[15]+1
                            while(i<vitri_dp[16]):
                                file_ghi.write(chuoi[i])
                                i=i+1
                            file_ghi.write(',')
                    elif(thuoctinh==17):
                        if(vitri_dp[16]==len(chuoi)):
                           file_ghi.write(',?')
                        else:
                            i = vitri_dp[16]+1
                            while(i<len(chuoi)):
                                file_ghi.write(chuoi[i])
                                i=i+1
                    thuoctinh=thuoctinh+1               
                file_ghi.write('\n')
        file_ghi.close()
    file_doc.close()
#truyen vao de chi file
file = 'test.txt'
PrepareData(file)
